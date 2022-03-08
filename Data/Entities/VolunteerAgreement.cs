using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class VolunteerAgreement : Entity
    {
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}
