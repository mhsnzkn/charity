using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Constants;

namespace Data.Dtos
{
    public class VolunteerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string CancellationReason { get; set; }
        public string Reason { get; set; }
        public List<Organisations> Organisations { get; set; }
        public List<Skills> Skills { get; set; }
        public string Status { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
