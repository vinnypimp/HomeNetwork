using System.Windows;
using System.Windows.Input;
using Authentication;
using NewUserRegistration.Model;
using NewUserRegistration.View;
using HomeNetwork.ViewModel;
using System.Security;
using System.Windows.Controls;
using System.Configuration;
using HomeNetwork.Resources;

namespace NewUserRegistration.ViewModel
{
    public class UserRegistrationViewModel : ViewModelBase
    {
        static string appName = ConfigurationManager.AppSettings["AppName"];

        #region Fields
        
        // Property Variables
        private NewUser _newUser;
        private string _authMessage;
        private bool _isVisible;
        
        #endregion

        #region Constructors
            
        public UserRegistrationViewModel()
        {
            this.Initialize();
        }

        #endregion  

        #region Command Properties

        private bool CanRegister()
        {
            // process Registration Validation
            return true;
        }

        private ICommand _register;
        public ICommand Register
        {
            get
            {
                if(_register == null)
                {
                    _register = new RelayCommand(
                        param => this.RegisterUser(param),
                        param => this.CanRegister());
                }
                return _register;
            }

        }

        #endregion

        #region Data Properties

        public NewUser NewUser
        {
            get { return _newUser; }
            
            set
            {
                _newUser = value;
                base.OnPropertyChanged("NewUser");
            }
        }

        public string AuthMessage
        {
            get { return _authMessage; }

            set
            {
                _authMessage = value;
                base.OnPropertyChanged("AuthMessage");
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }

            set
            {
                _isVisible = value;
                base.OnPropertyChanged("IsVisible");
            }
        }

        #endregion

        #region Event Handlers



        #endregion

        #region Private Methods

        private void Initialize()
        {
            UserRegistration urv = new UserRegistration();
            urv.DataContext = this;
            urv.Margin = new Thickness(0, 0, 0, 0);
            IsVisible = true;
            _newUser = new NewUser();
        }

        private void RegisterUser(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string passwd = passwordBox.Password;

            AuthService.Post svc = new AuthService.Post();
            _newUser.HashPassword = AuthenticationService.CalculateHash(passwd, _newUser.UserName);
            _newUser.AppName = appName;
            svc.PostData = _newUser;

            // send to webservice
            svc.RegisterNewUser();

            // if svc = success then update UI
            if (svc.Success)
            {
                _authMessage = _newUser.UserName + " Registered Successfully";

                IsVisible = false;
                StartUp.Login();
                
            }
        }

        #endregion  


    }
}
