using HomeNetwork.View;
using HomeNetwork.ViewModel;

namespace Authentication
{
    public void StartUp
    {


            var mainWindow = new MainWindow();
            var mvm = new MainWindowModel();
            mainWindow.DataContext = mvm;
            mainWindow.Show();
    }
}