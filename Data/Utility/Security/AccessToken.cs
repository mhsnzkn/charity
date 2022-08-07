using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utility.Security
{
    public class AccessToken
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int VolunteerId { get; set; }
    }
}
