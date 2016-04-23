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
    public class MachineViewModel : ViewModelBase
    {
        #region Fields

        // Property Variables
        private ObservableCollection<Machine> _machines;
        private int _machineCount;
        private Machine _selectedMachine;
        private DataTable _dt;
        private bool _addMachineCheck;
        private bool _modifyMachineCheck;

        #endregion

        #region Constructor

        public MachineViewModel()
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
                        param => this.AddNewMachine(),
                        param => this.CanGet());
                }
                return _addButton;
            }
        }

        #endregion

        #region Data Properties

        public ObservableCollection<Machine> Machines
        {
            get { return _machines; }

            set
            {
                _machines = value;
                base.OnPropertyChanged("MachineList");
            }
        }

        public Machine SelectedMachine 
        {
            get
            {
                return _selectedMachine;
            }
            set
            {
                if (_selectedMachine != value)
                {
                    _selectedMachine = value;
                    base.OnPropertyChanged("SelectedMachine");
                }
            }
        }

        public int MachineCount
        {
            get { return _machineCount; }

            set
            {
                _machineCount = value;
                base.OnPropertyChanged("MachineCount");
            }
        }

        public bool AddMachineCheck
        {
            get { return _addMachineCheck; }

            set
            {
                _addMachineCheck = value;
                base.OnPropertyChanged("AddMachineCheck");
                if (_addMachineCheck == true)
                {
                    SelectedMachine = new Machine();
                }
            }
        }

        public bool ModifyMachineCheck
        {
            get { return _modifyMachineCheck; }

            set
            {
                _modifyMachineCheck = value;
                base.OnPropertyChanged("ModifyMachineCheck");
            }
        }

        public DataTable dt { get; set; }

        #endregion

        #region Event Handlers

        void OnMachineListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update User Count
            this._machineCount = this._machines.Count;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Initialize UserView
            MachineView mvm = new MachineView();
            mvm.DataContext = this;
            mvm.Margin = new Thickness(0, 0, 0, 0);

            _selectedMachine = new Machine();

            // Get User List
            GetMachineList();

            // Add User Check
            AddMachineCheck = true;
        }

        private void GetMachineList()
        {

            // Get Users
            ServiceRequest.Get svc = new ServiceRequest.Get();
            //svc.RequestType = "GetUsers";
            //_dt = new DataTable();
            //DataSet ds = svc.GetRequestFile();
            svc.GetMachines();
            DataSet ds = svc.DS;
            
            // create new User Collection
            _machines = new ObservableCollection<Machine>();

            // Subscribe to CollectionChanged Event
            _machines.CollectionChanged += OnMachineListChanged;

            Machine mc = new Machine();
            _machines = (ObservableCollection<Machine>)mc.CreateMachineCollection(ds);
                  
            // Update Bindings
            base.OnPropertyChanged("MachineList");
        }

        private void GetMachine(Machine selectMachine)
        {
            // create User Object and send to web service then add user object to collection if web service returns 0 code
            // Code below adds new user to collection
            
            selectMachine.UserID = SelectedMachine.MachineID;
            selectMachine.Password = SelectedMachine.Password;
            selectMachine.FirstName = SelectedMachine.FirstName;
            selectMachine.LastName = SelectedMachine.LastName;
            selectMachine.MachineID = "1";
        }

        private void AddNewMachine()
        {
            Machine newMachine = new Machine();
            GetMachine(newMachine);
            // send to webservice
            ServiceRequest.Post svc = new ServiceRequest.Post();
            //svc.RequestType = "addNewUser";
            svc.PostData = newMachine;
            svc.AddNewMachine();
            // if rc = 0 then add to collection
            if (svc.Success)
            {
                Machines.Add(newMachine);
                base.OnPropertyChanged("MachineList");
            }
        }

        private void UpdateModifyCheck()
        {
            ModifyUserCheck = true;
        }

        #endregion
    }
}
