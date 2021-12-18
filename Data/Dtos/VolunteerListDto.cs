using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Constants.Enums;

namespace Data.Dtos
{
    public class VolunteerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public VolunteerStatus Status { get; set; }
        public string StatusName { get; set; }
        public string CancellationReason { get; set; }
    }
}
