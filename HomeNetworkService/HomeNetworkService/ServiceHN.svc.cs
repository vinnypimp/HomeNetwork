using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using HomeNetworkService.Models;
using HomeNetworkService.Data;

namespace HomeNetworkService
{
    public class ServiceHN : IServiceHN
    {
        #region Users
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
                    MachineName = user.MachineName,
                    Password = user.Password
                });
            }
            
            return results;
        }

        public wsSQLResult InsertNewUser(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Users user = jss.Deserialize<Users>(JSONdata);
                if (user == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                //if (user.UserID == Convert.ToString(dc.Users_HNUs.Where(i => i.UserID == user.UserID).FirstOrDefault()))
                //{
                //    // User Already Exists
                //    return -3;
                //}

                // Insert Record to SQL Server Table
                Users_HNU newUser = new Users_HNU()
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    MachineID = user.MachineID
                };

                dc.Users_HNUs.InsertOnSubmit(newUser);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult UpdateUser(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Users user = jss.Deserialize<Users>(JSONdata);
                if (user == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                Users_HNU currentUser = dc.Users_HNUs.Where(u => u.UserID == user.UserID).FirstOrDefault();
                if (currentUser == null)
                {
                    // Couldnt Find User to Update
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [Users_HNU] record with ID: " + user.UserID.ToString();
                    return result;
                }

                // Update Record to SQL Server Table
                currentUser.UserID = user.UserID;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Password = user.Password;
                currentUser.MachineID = user.MachineID;

                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }

        public wsSQLResult DeleteUser(string UserID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                Users_HNU user = dc.Users_HNUs.Where(u => u.UserID == UserID).FirstOrDefault();
                if (user == null)
                {
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [Users_HNU] record with ID: " + UserID.ToString();
                    return result;
                }

                dc.Users_HNUs.DeleteOnSubmit(user);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }
        #endregion

        #region Machines
        public List<Machines> GetMachines() 
       {
           HomeNetworkDataContext dc = new HomeNetworkDataContext();
           List<Machines> results = new List<Machines>();

           foreach (vwMachine machine in dc.vwMachines)
           {
               results.Add(new Machines()
               {
                   MachineID = machine.MachineID,
                   MachineName = machine.MachineName,
                   MacAddress = machine.MacAddress,
                   IPaddress = machine.IPaddress,
                   Static = Convert.ToString(machine.Static),
                   MachineType = machine.MachineType,
                   NetworkDeviceName = machine.NetworkDeviceName,
                   Location = machine.Location,
                   HostName = machine.HostName
               });
           }

           return results;
       }

        public wsSQLResult InsertNewMachine(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Machines machine = jss.Deserialize<Machines>(JSONdata);
                if (machine == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                //if (user.UserID == Convert.ToString(dc.Users_HNUs.Where(i => i.UserID == user.UserID).FirstOrDefault()))
                //{
                //    // User Already Exists
                //    return -3;
                //}

                // Insert Record to SQL Server Table
                Machines_HNM newMachine = new Machines_HNM()
                {
                    MachineID = machine.MachineID,
                    MachineName = machine.MachineName,
                    MacAddress = machine.MacAddress,
                    IPaddress = machine.IPaddress,
                    Static = Convert.ToBoolean(machine.Static),
                    MachineTypeID =machine.MachineTypeID,
                    NetworkDeviceID = machine.NetworkDeviceID,
                    LocationID = machine.LocationID,
                    HostName = machine.HostName
                };

                dc.Machines_HNMs.InsertOnSubmit(newMachine);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult UpdateMachine(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Machines machine = jss.Deserialize<Machines>(JSONdata);
                if (machine == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                Machines_HNM currentMach = dc.Machines_HNMs.Where(m => m.MachineID == machine.MachineID).FirstOrDefault();
                if (currentMach == null)
                {
                    // Couldnt Find User to Update
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [Machines_HNU] record with ID: " + machine.MachineID.ToString();
                    return result;
                }

                // Update Record to SQL Server Table
                   currentMach.MachineID = machine.MachineID;
                   currentMach.MachineName = machine.MachineName;
                   currentMach.MacAddress = machine.MacAddress;
                   currentMach.IPaddress = machine.IPaddress;
                   currentMach.Static = Convert.ToBoolean(machine.Static);
                   currentMach.MachineTypeID = machine.MachineTypeID;
                   currentMach.NetworkDeviceID = machine.NetworkDeviceID;
                   currentMach.LocationID = machine.LocationID;
                   currentMach.HostName = machine.HostName;
                   dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }

        public wsSQLResult DeleteMachine(string MachineID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                Machines_HNM machine = dc.Machines_HNMs.Where(m => m.MachineID == MachineID).FirstOrDefault();
                if (machine == null)
                {
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [Machines_HNM] record with ID: " + MachineID.ToString();
                    return result;
                }

                dc.Machines_HNMs.DeleteOnSubmit(machine);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }
        #endregion

        #region NetworkDevices
        public List<NetworkDevices> GetNetworkDevices()
       {
           HomeNetworkDataContext dc = new HomeNetworkDataContext();
           List<NetworkDevices> results = new List<NetworkDevices>();

            foreach (vwNetworkDevice netDevice in dc.vwNetworkDevices)
            {
                results.Add(new NetworkDevices()
                    {
                        NetworkDeviceID = netDevice.NetworkDeviceID,
                        NetworkDeviceName = netDevice.NetworkDeviceName,
                        MacAddress = netDevice.MacAddress,
                        IPaddress = netDevice.IPaddress,
                        NetworkDeviceType = netDevice.NetworkDeviceType,
                        Location = netDevice.Location,
                        NetworkPorts = (int)netDevice.NetworkPorts
                    });
            }
            return results;
       }

        public wsSQLResult InsertNewDevice(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                NetworkDevices netDevice = jss.Deserialize<NetworkDevices>(JSONdata);
                if (netDevice == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                //if (user.UserID == Convert.ToString(dc.Users_HNUs.Where(i => i.UserID == user.UserID).FirstOrDefault()))
                //{
                //    // User Already Exists
                //    return -3;
                //}

                // Insert Record to SQL Server Table
                NetworkDevices_HNND newDevice = new NetworkDevices_HNND()
                {
                    NetworkDeviceID = netDevice.NetworkDeviceID,
                    NetworkDeviceName = netDevice.NetworkDeviceName,
                    MacAddress = netDevice.MacAddress,
                    IPaddress = netDevice.IPaddress,
                    NetworkDeviceTypeID = netDevice.NetworkDeviceTypeID,
                    NetworkPorts = netDevice.NetworkPorts,
                    LocationID = netDevice.LocationID
                };

                dc.NetworkDevices_HNNDs.InsertOnSubmit(newDevice);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult UpdateNetDevice(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                NetworkDevices netDevice= jss.Deserialize<NetworkDevices>(JSONdata);
                if (netDevice == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                NetworkDevices_HNND currentDevice = dc.NetworkDevices_HNNDs.Where(n => n.NetworkDeviceID == netDevice.NetworkDeviceID).FirstOrDefault();
                if (currentDevice == null)
                {
                    // Couldnt Find User to Update
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [NetworkDevices_HNND] record with ID: " + netDevice.NetworkDeviceID.ToString();
                    return result;
                }

                // Update Record to SQL Server Table
                currentDevice.NetworkDeviceID = netDevice.NetworkDeviceID;
                currentDevice.NetworkDeviceName = netDevice.NetworkDeviceName;
                currentDevice.MacAddress = netDevice.MacAddress;
                currentDevice.IPaddress = netDevice.IPaddress;
                currentDevice.NetworkDeviceTypeID = netDevice.NetworkDeviceTypeID;
                currentDevice.NetworkPorts = netDevice.NetworkPorts;
                currentDevice.LocationID = netDevice.LocationID;
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }

        public wsSQLResult DeleteNetDevice(string NetDeviceID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                NetworkDevices_HNND netDevice = dc.NetworkDevices_HNNDs.Where(n => n.NetworkDeviceID == NetDeviceID).FirstOrDefault();
                if (netDevice == null)
                {
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [NetworkDevices_HNND] record with ID: " + NetDeviceID.ToString();
                    return result;
                }

                dc.NetworkDevices_HNNDs.DeleteOnSubmit(netDevice);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }
        #endregion

        #region IPtable
        public List<IPtable> GetIPtable()
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<IPtable> results = new List<IPtable>();

            foreach (HNSP_IPResult ip in dc.HNSP_IP())
            {
                results.Add(new IPtable()
                    {
                        IPaddress = ip.IPaddress,
                        DeviceName = ip.DeviceName,
                        HostName = ip.HostName,
                        MacAddress = ip.MacAddress,
                        NetworkDevice = ip.NetworkDevice
                    });
            }
            return results;
        }

        public List<IPtableInfo> GetIPtableInfo(string reqType, string devType, string ipAddress)
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<IPtableInfo> results = new List<IPtableInfo>();

            foreach (HNSP_IPinfoResult ip in dc.HNSP_IPinfo(reqType, devType, ipAddress))
            {
                results.Add(new IPtableInfo()
                {
                    IPaddressM = ip.IPaddressM,
                    MachineName= ip.MachineName,
                    HostName = ip.HostName,
                    Static = Convert.ToString(ip.Static),
                    MachineType = ip.MachineType,
                    NetworkDeviceName = ip.NetworkDeviceName,
                    LocationM = ip.LocationM,
                    IPaddressN = ip.IPaddressN,
                    MacAddress = ip.MacAddress,
                    NetworkPorts = (int)ip.NetworkPorts,
                    NetworkDeviceType = ip.NetworkDeviceType,
                    LocationN = ip.LocationN
                });
            }
            return results;
        }
        #endregion

        #region RouterSetups
        public List<RouterSetups> GetRouterSetups()
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<RouterSetups> results = new List<RouterSetups>();

            foreach (vwRouterSetup rs in dc.vwRouterSetups)
            {
                results.Add(new RouterSetups()
                {
                    SetupID = rs.SetupID,
                    SetupName = rs.SetupName
                });
            }
            return results;
        }

        //public List<RouterSetups> GetRouterNames()
        //{
        //    HomeNetworkDataContext dc = new HomeNetworkDataContext();
        //    List<RouterSetups> results = new List<RouterSetups>();

        //    foreach (vwRouterName rn in dc.vwRouterNames)
        //    {
        //        results.Add(new RouterSetups()
        //        {
        //            Name = rn.Name
        //        });
        //    }
        //    return results;
        //}

        public List<RouterSetups> GetRouterNames(string setupID)
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<RouterSetups> results = new List<RouterSetups>();

            foreach (HNSP_RouterSetupResult rn in dc.HNSP_RouterSetup("RL", setupID))
            {
                results.Add(new RouterSetups()
                {
                    Name = rn.Name
                });
            }
            return results;
        }

        public List<RouterSetups> GetRouterInfo(string setupID)
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<RouterSetups> results = new List<RouterSetups>();

            foreach (HNSP_RouterSetupResult ri in dc.HNSP_RouterSetup("RI", setupID))
            {
                results.Add(new RouterSetups()
                    {
                        SetupID = ri.SetupID,
                        SetupName = ri.SetupName,
                        Name = ri.Name,
                        IPaddress = ri.IPaddress,
                        UserID = ri.UserID,
                        Passwd = ri.Password,
                        SS24 = ri.SSID24,
                        PW24 = ri.Password24,
                        SS50 = ri.SSID50,
                        PW50 = ri.Password50,
                        EncryptionType = ri.EncryptionType,
                        Mode = ri.Mode,
                        Comments = ri.Comments
                    });
            }
            return results;
        }

        public wsSQLResult InsertNewRouterSetup(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                RouterSetups rs = jss.Deserialize<RouterSetups>(JSONdata);
                if (rs == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                //if (user.UserID == Convert.ToString(dc.Users_HNUs.Where(i => i.UserID == user.UserID).FirstOrDefault()))
                //{
                //    // User Already Exists
                //    return -3;
                //}

                // Insert Record to SQL Server Table
                RouterSetups_HNR newRS = new RouterSetups_HNR()
                {
                    SetupID = rs.SetupID,
                    SetupName = rs.SetupName,
                    Name = rs.Name,
                    IPaddress = rs.IPaddress,
                    UserID = rs.UserID,
                    Password = rs.Passwd,
                    SSID24 = rs.SS24,
                    Password24 = rs.PW24,
                    SSID50 = rs.SS50,
                    Password50 = rs.PW50,
                    EncryptionType = rs.EncryptionType,
                    Mode = rs.Mode,
                    Comments = rs.Comments
                };

                dc.RouterSetups_HNRs.InsertOnSubmit(newRS);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult UpdateRouterSetup(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                //Read JSON Stream into a String..
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // Convert to New User record..
                JavaScriptSerializer jss = new JavaScriptSerializer();
                RouterSetups rs = jss.Deserialize<RouterSetups>(JSONdata);
                if (rs == null)
                {
                    // Error: Couldn't deserialize JSON String
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                RouterSetups_HNR currentRS = dc.RouterSetups_HNRs.Where(r => r.SetupID == rs.SetupID).FirstOrDefault();
                if (currentRS == null)
                {
                    // Couldnt Find User to Update
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [RouterSetups_HNR] record with ID: " + rs.SetupID.ToString();
                    return result;
                }

                // Update Record to SQL Server Table
                currentRS.SetupID = rs.SetupID;
                currentRS.SetupName = rs.SetupName;
                currentRS.Name = rs.Name;
                currentRS.IPaddress = rs.IPaddress;
                currentRS.UserID = rs.UserID;
                currentRS.Password = rs.Passwd;
                currentRS.SSID24 = rs.SS24;
                currentRS.Password24 = rs.PW24;
                currentRS.SSID50 = rs.SS50;
                currentRS.Password50 = rs.PW50;
                currentRS.EncryptionType = rs.EncryptionType;
                currentRS.Mode = rs.Mode;
                currentRS.Comments = rs.Comments;
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }

        public wsSQLResult DeleteRouterSetup(string SetupID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                HomeNetworkDataContext dc = new HomeNetworkDataContext();
                RouterSetups_HNR rs = dc.RouterSetups_HNRs.Where(r => r.SetupID == SetupID).FirstOrDefault();
                if (rs == null)
                {
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [RouterSetups_HNR] record with ID: " + SetupID.ToString();
                    return result;
                }

                dc.RouterSetups_HNRs.DeleteOnSubmit(rs);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;  //Success
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;  //Failed
            }
        }
        #endregion
        
        #region ComboBoxItems

        public List<ComboBoxItems> GetCboItems(string reqType)
        {
            HomeNetworkDataContext dc = new HomeNetworkDataContext();
            List<ComboBoxItems> results = new List<ComboBoxItems>();

            foreach (HNSP_ComboBoxResult cbo in dc.HNSP_ComboBox(reqType))
            {
                results.Add(new ComboBoxItems()
                {
                    CboMember = cbo.cboMember,
                    CboDisplay = cbo.cboDisplay
                });
            }
            return results;
        }

        #endregion
    }
}
