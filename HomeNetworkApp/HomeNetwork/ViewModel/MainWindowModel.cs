using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HomeNetwork.Utilities;
using HomeNetwork.View;
using HomeNetwork.Models;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;

namespace HomeNetwork.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        //private UserViewModel _userView;
        public Control currentView;
        public MainWindow mainWindow;
        
        public MainWindowModel()
        {
            this.Initialize();
        }

        private void Initialize()
        {          
            
        }

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

        private IList<User> _users;
        private void GetUsers()
        {
            MessageBox.Show("Triggered a User Button Click");
            DisplayViewModel = new UserViewModel();
        }

        private ViewModelBase _displayViewModel;
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

        private IList<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
    }
}
