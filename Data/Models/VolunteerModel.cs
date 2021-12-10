using Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VolunteerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Reason { get; set; }
        public List<Organisations> Organisations { get; set; }
        public List<Skills> Skills { get; set; }
    }
}
