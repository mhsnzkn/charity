using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class VolunteerDetail : Entity
    {
        public Volunteer Volunteer { get; set; }
        public int VolunteerId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
