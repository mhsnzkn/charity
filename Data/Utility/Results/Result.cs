using Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utility.Results
{
    public class Result
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; } = UserMessages.Success;

        public Result SetError(string message)
        {
            if (Error)
            {
                this.Message += " " + message;
            }
            else
            {
                this.Error = true;
                this.Message = message;
            }
            return this;
        }

        public void AddMessage(string message)
        {
            this.Message += " "+message;
        }
    }
}
