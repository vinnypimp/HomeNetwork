using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.Models
{
    [DataContract]
    [Serializable]
    public class RouterSetups
    {
        [DataMember]
        public string SetupID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SetupName { get; set; }

        [DataMember]
        public string IPaddress { get; set; }

        [DataMember]
        public String UserID { get; set; }

        [DataMember]
        public string Passwd { get; set; }

        [DataMember]
        public string SS24 { get; set; }

        [DataMember]
        public string PW24 { get; set; }

        [DataMember]
        public string SS50 { get; set; }

        [DataMember]
        public string PW50 { get; set; }

        [DataMember]
        public string EncryptionType { get; set; }

        [DataMember]
        public string Mode { get; set; }

        [DataMember]
        public string Comments { get; set; }
    }
}