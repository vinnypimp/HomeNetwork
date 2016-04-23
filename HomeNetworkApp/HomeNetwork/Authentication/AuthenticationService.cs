using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Windows;
using Authentication.Model;

namespace Authentication
{
    public interface IAuthenticationService
    {
        AuthService.UserInfo AuthenticateUser(string username, string password);
    }
    public class AuthenticationService : IAuthenticationService
    {
        static string appName = ConfigurationManager.AppSettings["AppName"];

        public AuthService.UserInfo AuthenticateUser(string username, string clearTextPassword)
        {
            Credentials creds = new Credentials();
            creds.Username = username;
            creds.HashPassword = CalculateHash(clearTextPassword, username);
            creds.AppName = appName;

            AuthService.Post svc = new AuthService.Post();
            svc.PostData = creds;
            svc.ValidateUser();

            AuthService.UserInfo userData = new AuthService.UserInfo();
            userData = (AuthService.UserInfo)svc.ResponseData.validation;

            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide valid credentials.");
            
            return userData;
        }

        public static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the Hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Cng();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash to base64 encoded string to be compared to stored password
            return Convert.ToBase64String(hash);
        }
    }

    public class AuthService : JSON_Response
    {
        static string url = ConfigurationManager.AppSettings["Auth_Endpoint"];

        #region Fields

        // Property Variables
        private string _requestType;
        private string _jsonData;
        private Object _postData;
        private Validation _responseData;
        private UserInfo _userInfo;

        #endregion

        #region Constructor
        #endregion

        #region CommandProperties
        #endregion

        #region Data Properties

        public string RequestType
        {
            get { return _requestType; }

            set
            {
                _requestType = value;
            }
        }

        public string JsonData
        {
            get { return _jsonData; }

            set
            {
                _jsonData = value;
            }
        }

        public Object PostData
        {
            get
            {
                return _postData;
            }
            set
            {
                _postData = value;
            }
        }

        public Validation ResponseData
        {
            get
            {
                return _responseData;
            }
            set
            {
                _responseData = value;
            }
        }

        public UserInfo UserData
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        #endregion

        #region Private Methods

        private void PostRequest()
        {
            try
            {
                dynamic uri = url + RequestType;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                if (_postData != null)
                {
                    request.Method = "POST";
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(_postData.GetType());
                    Stream requestStream = request.GetRequestStream();
                    serializer.WriteObject(requestStream, _postData);
                    requestStream.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    }
                    if (RequestType == "ValidateUser")
                    {
                        Stream responseStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream);
                        string responseString = reader.ReadToEnd();
                        _responseData = JsonConvert.DeserializeObject<Validation>(responseString);
                        UserData = _responseData.validation;
                        if (UserData.ReturnCode == 1)
                        { UserData.IsAuthenticated = true; }
                    }
                    else
                    {
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(JSON_Response));
                        object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                        JSON_Response jsonResponse = objResponse as JSON_Response;
                        Success = jsonResponse.Success;
                        Exception = jsonResponse.Exception;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Post Requests

        public class Post : AuthService
        {
            public void ValidateUser()
            {
                RequestType = "ValidateUser";
                PostRequest();
            }

            public void RegisterNewUser()
            {
                RequestType = "RegisterNewUser";
                PostRequest();
            }
        }

        public class Validation
        {
            public UserInfo validation;
        }
        public class UserInfo
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string[] Roles { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int ReturnCode { get; set; }
            public string ReturnReason { get; set; }
            public bool IsAuthenticated { get; set; }
        }
        #endregion
    }

    #region Service Response

    [DataContract()]
    public class JSON_Response
    {
        [DataMember(Name = "Exception")]
        private string s_Exception;
        public string Exception
        {
            get { return s_Exception; }
            set { s_Exception = value; }
        }

        [DataMember(Name = "ReturnCode")]
        private int i_ReturnCode;
        public int ReturnCode
        {
            get { return i_ReturnCode; }
            set { i_ReturnCode = value; }
        }


        [DataMember(Name = "WasSuccessful")]
        private bool b_Success;
        public bool Success
        {
            get { return b_Success; }
            set { b_Success = value; }
        }
    }

    #endregion
}
