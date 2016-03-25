using System.Collections.Generic;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using Authentication.Helpers;
using Authentication;

namespace HomeNetwork.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        #region Fields

        private ViewModelBase _displayViewModel;

        #endregion

        #region Constructors

        public MainWindowModel()
        {
            this.Initialize();
            //Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        #endregion

        #region Command Properties

        private bool CanGet()
        {
            return true;
        }

        private ICommand _userButton;
        public ICommand UserButton
        {
            get
            {
                if (_userButton == null)
                {
                    _userButton = new RelayCommand(
                        param => this.GetUsers(),
                        param => this.CanGet());
                }
                return _userButton;
            }
        }

        #endregion  

        #region Data Properties

        public ViewModelBase DisplayViewModel
        {
            get
            {
                return _displayViewModel;
            }
            set
            {
                _displayViewModel = value;
                OnPropertyChanged("DisplayViewModel");
            }
        }

        //public Control currentView;
        //public MainWindow mainWindow;

        #endregion

        #region Event Handlers

        #endregion

        #region Private Methods

        private void Initialize()
        {
            //DisplayViewModel = new AuthenticationViewModel(new AuthenticationService());
            //Authentication.StartUp.Login();
        }

        private void GetUsers()
        {
            MessageBox.Show("Triggered a User Button Click");
            DisplayViewModel = new UserViewModel();
        }

        //private void MainWindow_Closing(object sender, CancelEventArgs e)
        //{
        //    Authentication.ShutDown.Logout();
        //}

        #endregion
    }
}
