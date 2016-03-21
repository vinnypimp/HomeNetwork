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
        public string RequestType { get; set; }
        public Object postData {get; set; }

    public DataSet GetRequest()
        {
            DataSet ds = new DataSet();
            string json = "";

        using (var client = new HttpClient())
        {
            var uri = url + RequestType;
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                json = responseContent.ReadAsStringAsync().Result;

                // Create Dataset
                ds = JsonConvert.DeserializeObject<DataSet>(json);
            }
        }
        return ds;
        }

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

    public void PostRequest()
    {
        try
        {
            dynamic uri = url + RequestType;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            if (postData != null)
            {
                request.Method = "POST";
                //request.ContentType = "application/json"
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(postData.GetType());
                Stream requestStream = request.GetRequestStream();
                serializer.WriteObject(requestStream, postData);
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






