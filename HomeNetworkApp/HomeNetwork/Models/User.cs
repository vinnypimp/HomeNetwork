using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Data;

namespace HomeNetwork.Models
{
    public class User : INotifyPropertyChanged
    {
        private string _userID;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _machineID;
        private string _machineName;

        public string UserID { get { return _userID; } set { _userID = value; OnPropertyChanged("UserID"); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstName"); } }
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        public string MachineID { get { return _machineID; } set { _machineID = value; OnPropertyChanged("MachineID"); } }
        public string MachineName { get { return _machineName; } set { _machineName = value; OnPropertyChanged("MachineName"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object CreateUserCollection(DataSet ds)
        {
            ObservableCollection<User> oc = new ObservableCollection<User>();
            DataTable dt = ds.Tables["Users"];

            foreach (DataRow row in dt.Rows)
            {
                oc.Add(new User
                {
                    UserID = row["UserID"].ToString(),
                    Password = row["Password"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    MachineID = row["MachineID"].ToString()
                });
            }
            return oc;
        }
    }
}
