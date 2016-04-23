using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.Models
{
    [DataContract]
    [Serializable]
    public class ComboBoxItems
    {
        [DataMember]
        public string CboMember { get; set; }

        [DataMember]
        public string CboDisplay { get; set; }
    }
}