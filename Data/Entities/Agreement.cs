using Data.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Agreement : Entity
    {
        [StringLength(100)]
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }

        public ICollection<VolunteerAgreement> VolunteerAgreements { get; set; }
    }
}
