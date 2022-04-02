using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VolunteerAgreementPostModel
    {
        public Guid Key { get; set; }
        public int[] AgreementIds { get; set; }
    }
}
