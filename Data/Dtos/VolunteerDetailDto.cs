using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class VolunteerDetailDto : VolunteerDto
    {
        public List<CommonFile> Files { get; set; }
        public List<AgreementTableDto> Agreements { get; set; }
    }
}
