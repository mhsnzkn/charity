using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{
    public class Constants
    {
        public struct UserRoles
        {
            public const string Admin = "admin";
            public const string Volunteer = "volunteer";
        }

        public struct HttpVolunteerActions
        {
            public const string Approve = "approve";
            public const string Cancel = "cancel";
        }
        public struct HttpUserActions
        {
            public const string EmailChange = "email";
            public const string PasswordChange = "password";
            public const string JobChange = "job";
        }


    }
}
