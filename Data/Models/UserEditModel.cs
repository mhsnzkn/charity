using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Data.Constants.Enums;

namespace Data.Models
{
    public class UserEditModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Job { get; set; }
        [Required]
        public string Role { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStatus Status { get; set; }
        public string Password { get; set; }
    }
}
