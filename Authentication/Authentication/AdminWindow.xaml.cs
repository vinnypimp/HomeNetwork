using System.Windows;
using System.Security.Permissions;

namespace Authentication
{

    [PrincipalPermission(SecurityAction.Demand, Role="Administrators")]
    public partial class AdminWindow : Window, IView
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        #region IView Members
        
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}
