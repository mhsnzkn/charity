using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{

    public struct UserRoles
    {
        public const string Admin = "E7BwbC6sz4wKnG";
        public const string Volunteer = "WpigdAQFU8f2HH";
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
    public struct HttpExpenseActions
    {
        public const string Approve = "approve";
        public const string Cancel = "cancel";
        public const string Pay = "pay";
    }

}
