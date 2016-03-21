using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.HomeNetwork
{
    [DataContract]
    [Serializable]
    public class IPtableInfo
    {
        [DataMember]
        public string IPaddressM { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public string Static { get; set; }

        [DataMember]
        public string MachineType { get; set; }

        [DataMember]
        public string NetworkDeviceName { get; set; }

        [DataMember]
        public string LocationM { get; set; }

        [DataMember]
        public string IPaddressN { get; set; }

        [DataMember]
        public string MacAddress { get; set; }

        [DataMember]
        public int NetworkPorts { get; set; }

        [DataMember]
        public string NetworkDeviceType { get; set; }

        [DataMember]
        public string LocationN { get; set; }
    }
}