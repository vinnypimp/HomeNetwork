using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HomeNetwork.ViewModel;
using Authentication;

namespace HomeNetwork.Resources
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Create a Custom Principal with Anonymous Identity at Startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            NavigationViewModel startup = new NavigationViewModel();
            startup.Initiate();
            startup.Login();

            //var mainWindow = new View.MainWindow();
            //var mvm = new MainWindowModel();
            //mainWindow.DataContext = mvm;
            //mvm.DisplayViewModel = new LoginViewModel(new AuthenticationService());
            //mainWindow.Show();

        }
    }
}
