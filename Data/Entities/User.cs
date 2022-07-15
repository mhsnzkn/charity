using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Constants;

namespace Data.Entities
{
    public class User : Entity
    {
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Job { get; set; }
        [StringLength(100)]
        public string Role { get; set; }
        public UserStatus Status { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
