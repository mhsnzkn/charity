using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Constants.Enums;

namespace Data.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
        public string StatusName { get; set; }
    }
}
