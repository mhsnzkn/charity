﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{
    public class UserMessages
    {
        public const string Success = "Success";
        public const string Fail = "Failed";
        public const string DataNotFound = "Data Not Found!";
        public const string ActionNotFound = "Action Not Found!";
        // Login
        public const string LoginFail = "Email or Password is wrong!";
        public const string UserNotActive = "User is not active!";

        // Volunteer
        public const string VolunteerRejected = "Volunteer's application already rejected!";
        public const string VolunteerCompleted = "Volunteer's application already completed!";

        // User
        public const string EmailExists = "Email is already registered!";
    }
}
