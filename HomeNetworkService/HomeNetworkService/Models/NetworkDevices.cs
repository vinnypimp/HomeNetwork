using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.Models
{
    [DataContract]
    [Serializable]
    public class NetworkDevices
    {
        [DataMember]
        public string NetworkDeviceID { get; set; }

        [DataMember]
        public string NetworkDeviceName { get; set; }

        [DataMember]
        public string MacAddress { get; set; }

        [DataMember]
        public string IPaddress { get; set; }

        [DataMember]
        public string NetworkDeviceType { get; set; }

        [DataMember]
        public int NetworkDeviceTypeID { get; set; }

        [DataMember]
        public int NetworkPorts { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int LocationID { get; set; }
     }
}