using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Authentication.Model
{
    [DataContract]
    [Serializable]
    public class Credentials
    {
        [DataMember]
        public string Username { get; set; }
        
        [DataMember]
        public string HashPassword { get; set; }

        [DataMember]
        public string AppName { get; set; }
    }
}
