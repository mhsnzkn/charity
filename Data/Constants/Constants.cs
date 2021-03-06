using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
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
        public const string OnHold = "onhold";
    }
    public struct HttpUserActions
    {
        public const string EmailChange = "email";
        public const string PasswordChange = "password";
        public const string JobChange = "job";
    }
    public struct CommonFileTypes
    {
        public const string DbsDocument = "DbsDocument";
        public const string Expense = "Expense";
    }


}
