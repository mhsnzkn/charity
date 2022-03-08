using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class VolunteerFile : Entity
    {
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public int CommonFileId { get; set; }
        public CommonFile CommonFile { get; set; }
    }
}
