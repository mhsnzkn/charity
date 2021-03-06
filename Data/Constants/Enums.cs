using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{

    public enum VolunteerStatus
    {
        Cancelled,
        OnHold,
        [Description("Application Pending")]
        ApplicationPending,
        DBS,
        DBSDocument,
        Induction,
        Agreement,
        Completed
    }
    public enum UserStatus
    {
        Passive,
        Active,
    }

    public enum ExpenseStatus
    {
        Cancelled,
        Pending,
        Accepted,
        Paid
    }

}