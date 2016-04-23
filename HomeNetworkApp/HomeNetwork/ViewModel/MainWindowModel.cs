using System.Collections.Generic;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using Authentication.Helpers;
using System.Windows.Controls;
using NewUserRegistration.ViewModel;

namespace HomeNetwork.ViewModel
{
    public class MainWindowModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Fields

        private ViewModelBase _displayViewModel;
        private HorizontalAlignment _hzAlign;
        private VerticalAlignment _vtAlign;
        private UserRegistrationViewModel _userReg;

        #endregion

        #region Constructors

        public MainWindowModel()
        {
            this.Initialize();
            UserRegistrationViewModel UserReg = new UserRegistrationViewModel();
            UserReg.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged);

            //Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        void PropertyChanged(object sender, PropertyChangedEventArgs e)
        { 
            if (e.PropertyName == "UserReg")
            {
                RegisterView();
            }
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

        public HorizontalAlignment HzAlign
        {
            get
            {
                return _hzAlign;
            }
            set
            {
                _hzAlign = value;
                OnPropertyChanged("HzAlign");
            }
        }

        public VerticalAlignment VtAlign
        {
            get
            {
                return _vtAlign;
            }
            set
            {
                _vtAlign = value;
                OnPropertyChanged("VtAlign");
            }
        }

        public UserRegistrationViewModel UserReg
        {
            get
            {
                return _userReg;
            }
            set
            {
                _userReg = value;
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
            
        }

        private void RegisterView()
        {
            DisplayViewModel = new UserRegistrationViewModel();
        }

        private void GetUsers()
        {
            MessageBox.Show("Triggered a User Button Click");
            _hzAlign = HorizontalAlignment.Left;
            _vtAlign = VerticalAlignment.Top;
            DisplayViewModel = new UserViewModel();
        }
        

        //private void MainWindow_Closing(object sender, CancelEventArgs e)
        //{
        //    Authentication.ShutDown.Logout();
        //}

        #endregion

        public static void Register()
        {
            
        }
    }
}
