using System.Collections.Generic;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;

namespace HomeNetwork.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        #region Fields

        private ViewModelBase _displayViewModel;
        private IList<User> _users;

        #endregion

        #region Constructors

        public MainWindowModel()
        {
            this.Initialize();
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

        }

        private void GetUsers()
        {
            MessageBox.Show("Triggered a User Button Click");
            DisplayViewModel = new UserViewModel();
        }

        #endregion
    }
}
