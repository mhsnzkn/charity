using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Constants.Enums;

namespace Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Job { get; set; }
        [StringLength(100)]
        public string Role { get; set; }
        public UserStatus Status { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
