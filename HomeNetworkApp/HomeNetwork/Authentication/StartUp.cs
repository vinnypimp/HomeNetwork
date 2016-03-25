using System;
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
            //Show the Login View
            
            //IView loginWindow = new LoginWindow(viewModel);
            //loginWindow.Show();
        }
        
        public static void StartApp(AuthenticationViewModel vm)
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
