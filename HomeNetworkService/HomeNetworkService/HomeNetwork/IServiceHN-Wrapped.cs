using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using JSONWebService.HomeNetwork;

namespace JSONWebService
{
    [ServiceContract]
    public interface IServiceHN
    {
        #region Users
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getUsers")]
        List<Users> GetUsers();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addNewUser")]
        wsSQLResult InsertNewUser(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "updateUser")]
        wsSQLResult UpdateUser(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deleteUser/{UserID}")]
        wsSQLResult DeleteUser(string UserID);
        #endregion

        #region Machines
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getMachines")]
        List<Machines> GetMachines();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addNewMachine")]
        wsSQLResult InsertNewMachine(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "updateMachine")]
        wsSQLResult UpdateMachine(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deleteMachine/{MachineID}")]
        wsSQLResult DeleteMachine(string MachineID); 
        #endregion

        #region NetDevices
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getNetworkDevices")]
        List<NetworkDevices> GetNetworkDevices();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addNewNetDevice")]
        wsSQLResult InsertNewDevice(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "updateNetDevice")]
        wsSQLResult UpdateNetDevice(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deleteNetDevice/{NetDeviceID}")]
        wsSQLResult DeleteNetDevice(string NetDeviceID);
        #endregion

        #region IPtable
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getIPtable")]
        List<IPtable> GetIPtable();
        #endregion

        #region RouterSetups
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getRouterSetups")]
        List<RouterSetups> GetRouterSetups();
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getRouterNames")]
        List<RouterSetups> GetRouterNames();
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "getRouterInfo/{setupID}")]
        List<RouterSetups> GetRouterInfo(string setupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addNewRouterSetup")]
        wsSQLResult InsertNewRouterSetup(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "updateRouterSetup")]
        wsSQLResult UpdateRouterSetup(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deleteRouterSetup/{SetupID}")]
        wsSQLResult DeleteRouterSetup(string SetupID);
        #endregion
    }
}
