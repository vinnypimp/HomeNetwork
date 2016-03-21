using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.HomeNetwork
{
    [DataContract]
    [Serializable]
    public class IPtable
    {
        [DataMember]
        public string IPaddress { get; set; }

        [DataMember]
        public string DeviceName { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public string MacAddress { get; set; }

        [DataMember]
        public string NetworkDevice { get; set; }
     }
}