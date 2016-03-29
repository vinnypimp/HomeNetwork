using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HomeNetworkService.Models
{
    [DataContract]    
    [Serializable]
        public class Users
    {
        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string MachineID { get; set; }

        //[DataMember]
        //public string UserPW { get; set; }
        
    }
}