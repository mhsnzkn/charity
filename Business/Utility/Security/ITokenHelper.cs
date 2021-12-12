using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utility.Security
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}
