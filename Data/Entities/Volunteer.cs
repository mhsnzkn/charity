using Data.Abstract;
using Data.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Data.Constants.Enums;

namespace Data.Entities
{
    public class Volunteer : IEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string PostCode { get; set; }

        [StringLength(15)]
        public string MobileNumber { get; set; }

        [StringLength(15)]
        public string HomeNumber { get; set; }

        [StringLength(100)]
        public string CancellationReason { get; set; }

        [StringLength(1000)]
        public string Reason { get; set; }
        public Guid Key { get; set; } = Guid.NewGuid();
        public Organisations[] Organisations { get; set; }
        public Skills[] Skills { get; set; }
        public VolunteerStatus Status { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime? UptDate { get; set; }
        public int? UptUsr { get; set; }

    }
}
