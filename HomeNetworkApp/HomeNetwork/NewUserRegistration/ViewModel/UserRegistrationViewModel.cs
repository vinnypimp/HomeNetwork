using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Authentication;
using NewUserRegistration.Model;
using NewUserRegistration.View;
using Authentication;

namespace NewUserRegistration.ViewModel
{
    public class UserRegistrationViewModel : ViewModelBase
    {
        #region Fields
        
        // Property Variables
        private NewUser _newUser;

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

        private ICommand _process;
        public ICommand Process
        {
            get
            {
                if(_process == null)
                {
                    _process = new RelayCommand(
                        param => this._RegisterUser(),
                        param => this.CanRegister());
                }
                return _process;
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

        #endregion

        #region Event Handlers



        #endregion

        #region Private Methods

        private void Initialize()
        {
            UserRegistration urv = new UserRegistration();
            urv.DataContext = this;
            urv.Margin = new Thickness(0, 0, 0, 0);

            _newUser = new NewUser();
        }

        private void RegisterUser(NewUser user)
        {
            
        }

        #endregion  


    }
}
