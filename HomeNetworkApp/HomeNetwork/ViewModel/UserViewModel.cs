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
    public class UserViewModel : ViewModelBase
    {
        #region Fields

        // Property Variables
        private ObservableCollection<User> _users;
        private int _userCount;
        private User _selectedUser;
        private bool _addUserCheck;
        private bool _modifyUserCheck;

        #endregion

        #region Constructor

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

        private ICommand _addButton;
        public ICommand AddButton
        {
            get
            {
                if (_addButton == null)
                {
                    _addButton = new RelayCommand(
                        param => this.AddNewUser(),
                        param => this.CanGet());
                }
                return _addButton;
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

        public DataTable dt { get; set; }

        #endregion

        #region Event Handlers

        void OnUserListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update User Count
            this._userCount = this._users.Count;
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
        }

        private void GetUserList()
        {

            // Get Users
            ServiceRequest.Get svc = new ServiceRequest.Get();
            //svc.RequestType = "GetUsers";
            //_dt = new DataTable();
            svc.GetRequestFile();
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
            selectUser.MachineID = "1";
        }

        private void AddNewUser()
        {
            User newUser = new User();
            GetUser(newUser);
            // send to webservice
            ServiceRequest.Post svc = new ServiceRequest.Post();
            //svc.RequestType = "addNewUser";
            svc.PostData = newUser;
            svc.AddNewUser();
            // if rc = 0 then add to collection
            if (svc.Success)
            {
                Users.Add(newUser);
                base.OnPropertyChanged("UserList");
            }
        }

        private void UpdateModifyCheck()
        {
            ModifyUserCheck = true;
        }

        #endregion
    }
}
