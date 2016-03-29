using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using HomeNetworkService.Models;

namespace HomeNetworkService
{
    [ServiceContract]
    public interface IServiceHN
    {
        #region Users
        [OperationContract]
        [return:MessageParameter(Name="Users")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped,
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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "deleteUser/{UserID}")]
        wsSQLResult DeleteUser(string UserID);
        #endregion

        #region Machines
        [OperationContract]
        [return: MessageParameter(Name = "Machines")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "deleteMachine/{MachineID}")]
        wsSQLResult DeleteMachine(string MachineID); 
        #endregion

        #region NetDevices
        [OperationContract]
        [return: MessageParameter(Name = "NetDevices")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "deleteNetDevice/{NetDeviceID}")]
        wsSQLResult DeleteNetDevice(string NetDeviceID);
        #endregion

        #region IPtable
        [OperationContract]
        [return: MessageParameter(Name = "IPtable")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getIPtable")]
        List<IPtable> GetIPtable();

        [OperationContract]
        [return: MessageParameter(Name = "IPtableInfo")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getIPtableInfo/{reqType}/{devType}/{ipAddress}")]
        List<IPtableInfo> GetIPtableInfo(string reqType, string devType, string IPaddress);
        #endregion

        #region RouterSetups
        [OperationContract]
        [return: MessageParameter(Name = "RouterSetups")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getRouterSetups")]
        List<RouterSetups> GetRouterSetups();
        
        [OperationContract]
        [return: MessageParameter(Name = "RouterNames")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getRouterNames/{setupID}")]
        List<RouterSetups> GetRouterNames(string setupID);

        [OperationContract]
        [return: MessageParameter(Name = "RouterInfo")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "deleteRouterSetup/{SetupID}")]
        wsSQLResult DeleteRouterSetup(string SetupID);
        #endregion

        #region ComboBoxItems
        [OperationContract]
        [return: MessageParameter(Name = "CBO")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getCboItems/{reqType}")]
        List<ComboBoxItems> GetCboItems(string reqType);
        #endregion
    }
}
