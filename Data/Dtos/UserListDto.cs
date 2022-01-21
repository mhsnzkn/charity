using Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Role { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStatus Status { get; set; }
    }
}
