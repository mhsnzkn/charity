using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    public class Result
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; }

        public void SetError(string message)
        {
            this.Error = true;
            this.Message = message;
        }
    }
}
