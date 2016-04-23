using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.Models
{
    [DataContract]
    [Serializable]
        public class Machines
    {
        [DataMember]
        public string MachineID { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string MacAddress { get; set; }

        [DataMember]
        public string IPaddress { get; set; }

        [DataMember]
        public string Static { get; set; }

        [DataMember]
        public string MachineType { get; set; }

        [DataMember]
        public int MachineTypeID { get; set; }

        [DataMember]
        public string NetworkDeviceName { get; set; }

        [DataMember]
        public string NetworkDeviceID { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public string HostName { get; set; }
     }
 }