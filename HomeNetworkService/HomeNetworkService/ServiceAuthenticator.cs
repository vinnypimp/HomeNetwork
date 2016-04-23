using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace HomeNetworkService
{
    public class ServiceAuthenticator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            // validate arguments
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // check if user is not test
            if (userName != "1" || password != "1")
                throw new SecurityTokenException("Unknown Username or Password");
        }
    }
}