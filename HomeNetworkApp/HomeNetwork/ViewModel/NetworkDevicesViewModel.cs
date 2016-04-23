using System.Collections.ObjectModel;
using HomeNetwork.Utilities;
using HomeNetwork.View;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;
using HomeNetwork.Services;
using System.Collections.Generic;
using System.Data;
using HomeNetwork.Helpers;

namespace HomeNetwork.ViewModel
{
    public class NetworkDeviceViewModel : ViewModelBase
    {
        #region Fields

        // Property Variables
        private ObservableCollection<NetDevice> _netDevices;
        private int _netDeviceCount;
        private NetDevice _selectedNetDevice;
        private DataTable _dt;
        private bool _addNetDeviceCheck;
        private bool _modifyNetDeviceCheck;

        #endregion

        #region Constructor

        public NetworkDeviceViewModel()
        {
            this.Initialize();
        }

        public void RowSelected()
        {
            this.UpdateModifyCheck();
        }

        #endregion

        #region Command Properties

        private bool CanGet()
        {
            return true;
        }

        private ICommand _addButton;
        public ICommand AddButton
        {
            get
            {
                if (_addButton == null)
                {
                    _addButton = new RelayCommand(
                        param => this.AddNewNetDevice(),
                        param => this.CanGet());
                }
                return _addButton;
            }
        }

        #endregion

        #region Data Properties

        public ObservableCollection<NetDevice> NetDevices
        {
            get { return _netDevices; }

            set
            {
                _netDevices = value;
                base.OnPropertyChanged("NetworkDeviceList");
            }
        }

        public NetDevice SelectedNetDevice 
        {
            get
            {
                return _selectedNetDevice;
            }
            set
            {
                if (_selectedNetDevice != value)
                {
                    _selectedNetDevice = value;
                    base.OnPropertyChanged("SelectedNetDevice");
                }
            }
        }

        public int NetDeviceCount
        {
            get { return _netDeviceCount; }

            set
            {
                _netDeviceCount = value;
                base.OnPropertyChanged("NetDeviceCount");
            }
        }

        public bool AddNetDeviceCheck
        {
            get { return _addNetDeviceCheck; }

            set
            {
                _addNetDeviceCheck = value;
                base.OnPropertyChanged("AddNetDeviceCheck");
                if (_addNetDeviceCheck == true)
                {
                    SelectedNetDevice = new NetDevice();
                }
            }
        }

        public bool ModifyNetDeviceCheck
        {
            get { return _modifyNetDeviceCheck; }

            set
            {
                _modifyNetDeviceCheck = value;
                base.OnPropertyChanged("ModifyNetDeviceCheck");
            }
        }

        public DataTable dt { get; set; }

        #endregion

        #region Event Handlers

        void OnNetworkDeviceListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update User Count
            this._netDeviceCount = this._netDevices.Count;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Initialize UserView
            NetworkDeviceView ndvm = new NetworkDeviceView();
            ndvm.DataContext = this;
            ndvm.Margin = new Thickness(0, 0, 0, 0);

            _selectedNetDevice = new NetDevice();

            // Get User List
            GetNetDeviceList();

            // Add User Check
            AddNetDeviceCheck = true;
        }

        private void GetNetDeviceList()
        {

            // Get Network Devices
            ServiceRequest.Get svc = new ServiceRequest.Get();
            //svc.RequestType = "GetUsers";
            //_dt = new DataTable();
            //DataSet ds = svc.GetRequestFile();
            svc.GetNetDevices();
            DataSet ds = svc.DS;
            
            // create new User Collection
            _netDevices = new ObservableCollection<NetDevice>();

            // Subscribe to CollectionChanged Event
            _netDevicess.CollectionChanged += OnNetworkDeviceListChanged;

            NetDevice ndc = new NetDevice();
            _netDevices = (ObservableCollection<NetDevice>)ndc.CreateNetDeviceCollection(ds);
                  
            // Update Bindings
            base.OnPropertyChanged("NetworkDeviceList");
        }

        private void GetNetDevice(NetDevice selectNetDevice)
        {
            // create User Object and send to web service then add user object to collection if web service returns 0 code
            // Code below adds new user to collection
            
            selectNetDevice.UserID = SelectedNetDevice.MachineID;
            selectNetDevice.Password = SelectedNetDevice.Password;
            selectNetDevice.FirstName = SelectedNetDevice.FirstName;
            selectNetDevice.LastName = SelectedNetDevice.LastName;
            selectNetDevice.MachineID = "1";
        }

        private void AddNewNetDevice()
        {
            NetDevice newNetDevice = new NetDevice();
            GetNetDevice(newNetDevice);
            // send to webservice
            ServiceRequest.Post svc = new ServiceRequest.Post();
            //svc.RequestType = "addNewUser";
            svc.PostData = newNetDevice;
            svc.AddNewNetDevice();
            // if rc = 0 then add to collection
            if (svc.Success)
            {
                NetDevices.Add(newNetDevice);
                base.OnPropertyChanged("NetworkDeviceList");
            }
        }

        private void UpdateModifyCheck()
        {
            ModifyUserCheck = true;
        }

        #endregion
    }
}
