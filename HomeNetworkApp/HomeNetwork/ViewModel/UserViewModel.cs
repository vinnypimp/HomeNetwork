using System.Collections.ObjectModel;
using HomeNetwork.View;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;
using HomeNetwork.Services;
using System.Data;
using System.Collections.Generic;
using HomeNetwork.Helpers;

namespace HomeNetwork.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        #region Fields

        // Property Variables
        private ObservableCollection<User> _users;
        private int _userCount;
        private User _selectedUser;
        private bool _addUserCheck;
        private bool _modifyUserCheck;
        private string _machineID;
        private string _machineName;
        
        #endregion

        #region Constructors

        public UserViewModel()
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

        private ICommand _process;
        public ICommand Process
        {
            get
            {
                if (_process == null)
                {
                    _process = new RelayCommand(
                        param => this.ProcessUser(),
                        param => this.CanGet());
                }
                return _process;
            }
        }

        #endregion

        #region Data Properties

        public ObservableCollection<User> Users
        {
            get { return _users; }

            set
            {
                _users = value;
                base.OnPropertyChanged("UserList");
            }
        }

        public User SelectedUser 
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    base.OnPropertyChanged("SelectedUser");
                }
            }
        }

        public int UserCount
        {
            get { return _userCount; }

            set
            {
                _userCount = value;
                base.OnPropertyChanged("UserCount");
            }
        }

        public bool AddUserCheck
        {
            get { return _addUserCheck; }

            set
            {
                _addUserCheck = value;
                base.OnPropertyChanged("AddUserCheck");
                if (_addUserCheck == true)
                {
                    SelectedUser = new User();
                }
            }
        }

        public bool ModifyUserCheck
        {
            get { return _modifyUserCheck; }

            set
            {
                _modifyUserCheck = value;
                base.OnPropertyChanged("ModifyUserCheck");
            }
        }

        public string MachineID
        {
            get { return _machineID; }
            set
            {
                if (_machineID !=value)
                {
                    _machineID = value;
                    base.OnPropertyChanged("MachineID");
                }
            }
        }

        public string MachineName
        {
            get { return _machineName; }

            set
            {
                _machineName = value;
                base.OnPropertyChanged("MachineName");
            }
        }

        public DataTable dt { get; set; }

        public List<ComboBoxItems> CboItems { get; set; }
      
        #endregion

        #region Event Handlers

        void OnUserListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update User Count
            this._userCount = this._users.Count;

            // notified when collection changes
        }

        #endregion

        #region Private Methods

        public void Initialize()
        {
            // Initialize UserView
            UserView uvm = new UserView();
            uvm.DataContext = this;
            uvm.Margin = new Thickness(0, 0, 0, 0);

            _selectedUser = new User();

            // Get User List
            GetUserList();

            // Add User Check
            AddUserCheck = true;

            // Load Machine Name ComboBox
            FillComboBoxes();
        }

        private void GetUserList()
        {

            // Get Users
            ServiceRequest.Get svc = new ServiceRequest.Get();
            //svc.RequestType = "GetUsers";
            //_dt = new DataTable();
            svc.GetRequestFile("U");
            //svc.GetUsers();
            DataSet ds = svc.DS;
            
            // create new User Collection
            _users = new ObservableCollection<User>();

            // Subscribe to CollectionChanged Event
            _users.CollectionChanged += OnUserListChanged;

            User uc = new User();
            _users = (ObservableCollection<User>)uc.CreateUserCollection(ds);
                  
            // Update Bindings
            base.OnPropertyChanged("UserList");
        }

        private void GetUser(User selectUser)
        {           
            selectUser.UserID = SelectedUser.UserID;
            selectUser.Password = SelectedUser.Password;
            selectUser.FirstName = SelectedUser.FirstName;
            selectUser.LastName = SelectedUser.LastName;
            selectUser.MachineID = SelectedUser.MachineID;
        }

        private void ProcessUser()
        {
            User selectUser = new User();
            GetUser(selectUser);
            if (_addUserCheck)
            {
                AddNewUser(selectUser);
            }
            else if (_modifyUserCheck)
            {
                UpdateUser(selectUser);
            }
        }

        private void AddNewUser(User newUser)
        {
            ServiceRequest.Post svc = new ServiceRequest.Post();
            svc.PostData = newUser;

            // send to webservice
            svc.AddNewUser();

            // if rc = 0 then add to collection
            if (svc.Success)
            {
                newUser.MachineName = SelectedUser.MachineName;
                Users.Add(newUser);
                base.OnPropertyChanged("UserList");
            }
        }

        private void UpdateUser(User user)
        {
            ServiceRequest.Post svc = new ServiceRequest.Post();
            svc.PostData = user;

            // send to webservice
            svc.UpdateUser();

            // if rc = 0 then add to collection
            if (svc.Success)
            {
                base.OnPropertyChanged("UserList");
            }
            else
            {
                this.Initialize();
            }
        }

        private void UpdateModifyCheck()
        {
            ModifyUserCheck = true;
        }

        private void FillComboBoxes()
        {
            ServiceRequest.Get svc = new ServiceRequest.Get();
            svc.GetRequestFile("C");
            DataTable dt = new DataTable();
            dt = svc.DS.Tables[0];

            CboItems = dt.ToList<ComboBoxItems>();
        }

        #endregion
    }
}
