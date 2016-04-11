using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace NewUserRegistration.Model
{
    public class NewUser : INotifyPropertyChanged
    {
        private string _userName;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _email;

        public string UserName { get { return _userName; } set { _userName = value; OnPropertyChanged("UserName"); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstName"); } }
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        public string Email { get { return _email; } set { _email= value; OnPropertyChanged("Email"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
