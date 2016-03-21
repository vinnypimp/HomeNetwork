using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using HomeNetwork.Models;
using System.Configuration;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.Net;

namespace HomeNetwork.Services
{
    #region Service Request
       
    public class ServiceRequest : JSON_Response
    {
        static string url = ConfigurationManager.AppSettings["HNWS_Endpoint"];
        
    #region Fields
        
        // Property Variables
        private string _requestType;
        private string _jsonData;
        private DataSet _ds;
        private Object _postData;
        
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
         
         public DataSet DS
         {
             get { return _ds; }
             
             set
             {
                 _ds = value;
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
         
    #endregion
         
    #region Private Methods
         
         private void GetRequest()
         {
             using (var client = new HttpClient())
             {
                 var uri = url + _requestType;
                 var response = client.GetAsync(uri).Result;
                 
                 if (response.IsSuccessStatusCode)
                 {
                     var responseContent = response.Content;
                     _jsonData = responseContent.ReadAsStringAsync().Result;
                     
                     // Create DataSet
                     _ds = JsonConvert.DeserializeObject<DataSet>(_jsonData);
                 }
             }
         }
         
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
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(JSON_Response));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    JSON_Response jsonResponse = objResponse as JSON_Response;
                    Success = jsonResponse.Success;
                    Exception = jsonResponse.Exception;
                }
            }
            catch (Exception ex)
            {
            //throw new Exception(ex.ToString);
            }
         }
         
    #endregion
    
    #region Get Requests
    
        public class Get : ServiceRequest
    {
            
        public void GetUsers()
        {
            RequestType = "GetUsers";
            GetRequest();
         }
            
         public void GetMachines()
         {
            RequestType = "GetMachines";
            GetRequest();
          }
         public void GetNetDevices()
         {
            RequestType = "GetNetDevices";
            GetRequest();
          }          
    }
    
    #endregion
        
    #region Post Requests
    
    public class Post : ServiceRequest
    {
            
        public void AddNewUser()
        {
            RequestType = "AddNewUser";
            PostRequest();
        }
            
        public void AddNewMachine()
        {
            RequestType = "AddNewMachine";
            PostRequest();
        }
        
        public void AddNewNetDevice()
        {
            RequestType = "AddNewNetDevice";
            PostRequest();
        }
    }
    
    #endregion    

    public DataSet GetRequestFile()
    {
        DataSet ds = new DataSet();
        string fileName = @"../../Data/UserJson.txt";
        using (StreamReader client = new StreamReader(fileName))
        {
            string json = client.ReadToEnd();
           
            // Create Dataset
            ds = JsonConvert.DeserializeObject<DataSet>(json);
            }
        return ds;
    }
    
}

    #endregion

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






