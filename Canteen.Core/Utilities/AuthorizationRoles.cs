using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Utilities
{
    public class AuthoriationRoles
    {
        public enum Roles
        {
            Customer = 1, Vendor, Watcher, Admin
        }
        public const string default_username = "user_canteen";
        public const string default_email = "user@canteenapp.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Customer;
    }
}


