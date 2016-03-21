using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HomeNetwork.ViewModel;

namespace HomeNetwork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize Main Window and View Model
            //var mainWindow = new MainWindow();
            var mvm = new MainWindowModel();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mvm;
            mainWindow.Show();
            //mainWindow.DataContext = viewModel;
            //mainWindow.Show();
        }
    }
}
