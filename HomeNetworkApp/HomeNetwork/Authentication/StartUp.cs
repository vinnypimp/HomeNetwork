using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeNetwork.Resources;
using HomeNetwork.View;
using HomeNetwork.ViewModel;

namespace Authentication
{
    public static class StartUp
    {
        public static void Login()
        {
            // Load Main Window with Login View
            var mainWindow = new MainWindow();
            var mvm = new MainWindowModel();
            mainWindow.DataContext = mvm;
            mvm.DisplayViewModel = new LoginViewModel(new AuthenticationService());
            mvm.HzAlign = HorizontalAlignment.Center;
            mvm.VtAlign = VerticalAlignment.Top;
            mainWindow.Show();
        }
        
        public static void StartApp(LoginViewModel vm)
        {
            if (vm.IsAuthenticated)
            {
                // Load Main Window
                var mainWindow = new MainWindow();
                var mvm = new MainWindowModel();
                mainWindow.DataContext = mvm;
                mainWindow.Show();

                App.Current.MainWindow.Close();
                App.Current.MainWindow = mainWindow;
            }
        }
    }

    public static class ShutDown
    {
        public static void Logout()
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
            }
        }
    }
}
