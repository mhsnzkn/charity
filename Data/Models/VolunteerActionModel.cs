using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VolunteerActionModel
    {
        public int Id { get; set; }
        public string CancellationReason { get; set; }
        public string Action { get; set; }
    }
}
