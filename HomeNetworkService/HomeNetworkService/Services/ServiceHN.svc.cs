using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using JSONWebService.HomeNetwork;

namespace JSONWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class ServiceHN : IServiceHN
    {
       public List<Users> GetUsers()
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<Users> results = new List<Users>();

            foreach (vwUser user in dc.vwUsers)
            {
                results.Add(new Users()
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MachineName = user.MachineName
                });
            }
            
            return results;
        }
    }
}
