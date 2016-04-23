using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using Authentication.Helpers;
using HomeNetwork.View;
using HomeNetwork.ViewModel;
using NewUserRegistration.ViewModel;

namespace Authentication
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Fields

        // Property Variables
        private readonly IAuthenticationService _authenticationService;
        private readonly DelegateCommand _loginCommand;
        private readonly DelegateCommand _logoutCommand;
        private readonly DelegateCommand _register;

        private string _username;
        private string _status;
        private bool _txtAuthVisible;
        private string _loginHeader;
        private MainWindowModel parentViewModel;


        #endregion

        #region Constructors

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new DelegateCommand(Login, CanLogin);
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _register = new DelegateCommand(RegisterView);
            this.Initialize();
        }

        #endregion

        #region Command Properties

        private bool CanGet()
        {
            return true;
        }

        #endregion

        #region Data Properties

        public string Username
        {
            get { return _username; }
            set { _username = value; base.OnPropertyChanged("Username"); }
        }

        public bool TxtAuthVisible
        {
            get { return _txtAuthVisible; }
            set { _txtAuthVisible = value; base.OnPropertyChanged("TxtAuthVisible"); }
        }

        public string LoginHeader
        {
            get { return _loginHeader; }
            set { _loginHeader = value; base.OnPropertyChanged("LoginHeader"); }
        }

        public string Status
        {
            get
            {
                //if (!IsAuthenticated)
                //{ return "Not Authenticated!"; }
                return _status;
            }
            set { _status = value; base.OnPropertyChanged("Status"); }
        }

        public MainWindowModel ParentViewModel
        {
            get { return parentViewModel; }
            set { parentViewModel = value; OnPropertyChanged("ParentViewModel"); }
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get { return _loginCommand; } }
        public DelegateCommand LogoutCommand { get { return _logoutCommand; } }
        public DelegateCommand Register { get { return _register; } }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Initialize Login View
            LoginView login = new LoginView();
            login.DataContext = this;
            LoginHeader = "Home Network Login";

            
        }

        private void Login(object parameter)
        {
            TxtAuthVisible = true;
            NotifyPropertyChanged("TxtAuthVisible");

            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            try
            {
                // Validate credentials through the authentication service
                AuthService.UserInfo user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

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
                _status = "Login Failed! Please provide valid credentials.";
                NotifyPropertyChanged("Status");
            }
            catch (Exception ex)
            {
                _status = string.Format("Error: {0}", ex.Message);
            }
        }

        private void RegisterView(object parameter)
        {
            StartUp.Registration();
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
