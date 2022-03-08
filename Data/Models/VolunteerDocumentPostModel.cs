using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VolunteerDocumentPostModel
    {
        public Guid Key { get; set; }
        public IFormFile[] Files { get; set; }
    }
}
