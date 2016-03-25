using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Security;
using Authentication.Helpers;
using HomeNetwork.ViewModel;
using System.Windows;

namespace Authentication
{
    //public interface IViewModel { }

    public class AuthenticationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly DelegateCommand _loginCommand;
        private readonly DelegateCommand _logoutCommand;

        private string _username;
        private string _status;
        private bool _txtAuthVisible;
        private string _loginHeader;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new DelegateCommand(Login, CanLogin);
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            //_showViewCommand = new DelegateCommand(ShowView, null);
            this.Initialize();
        }

        #region Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }

        public bool TxtAuthVisible
        {
            get { return _txtAuthVisible; }
            set { _txtAuthVisible = value; NotifyPropertyChanged("TxtAuthVisible"); }
        }

        public string LoginHeader {get; set;}
            
        //public string AuthenticatedUser
        //{
        //    get
        //    {
        //        if (IsAuthenticated)
        //            return string.Format("Signed in as {0}. {1}",
        //                Thread.CurrentPrincipal.Identity.Name,
        //                Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an Administrator!"
        //                    : "You are NOT a member of the Administrators Group.");
        //        return "Not Authenticated!";
        //    }
        //}

        public string Status
        {
            get
            {
                if (!IsAuthenticated)
                { return "Not Authenticated!"; }
                return _status;
            }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get { return _loginCommand; } }
        public DelegateCommand LogoutCommand { get { return _logoutCommand; } }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Initialize Login View
            Login login = new Login();
            login.DataContext = this;
            login.Margin = new Thickness(0, 0, 0, 0);
            login.HorizontalAlignment = HorizontalAlignment.Center;
            login.VerticalAlignment = VerticalAlignment.Center;

            _loginHeader = "Home Network Login:";

        }

        private void Login(object parameter)
        {
            TxtAuthVisible = true;

            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            try
            {
                // Validate credentials through the authentication service
                User user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

                // Get the Current Principal Object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                // Authenticate the User
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Email, user.Roles);

                // Update UI
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;

                // Start Application
                StartUp.StartApp(this);
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login Failed! Please provide valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("Error: {0}", ex.Message);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        #endregion

        public void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
