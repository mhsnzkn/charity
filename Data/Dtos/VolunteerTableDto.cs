using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Data.Constants;

namespace Data.Dtos
{
    public class VolunteerTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VolunteerStatus Status { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
