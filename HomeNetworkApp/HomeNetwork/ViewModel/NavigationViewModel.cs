using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Authentication;
using HomeNetwork.View;

namespace HomeNetwork.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        #region Fields
        private MainWindow _mainWindow;
        private MainWindowModel _mvm;
        #endregion

        #region Constructors
        public void Initiate()
        {
            MainWindow = new MainWindow();
            MVM = new MainWindowModel();
            MainWindow.DataContext = MVM;
        }
        #endregion  

        #region Data Properties
        public MainWindow MainWindow { get; set; }
        public MainWindowModel MVM { get; set; }
        #endregion

        #region Methods
        public void Login()
        {
            MVM.DisplayViewModel = new LoginViewModel(new AuthenticationService());
            MVM.HzAlign = HorizontalAlignment.Center;
            MVM.VtAlign = VerticalAlignment.Top;
            MainWindow.Show();
        }


        #endregion
    }
}
