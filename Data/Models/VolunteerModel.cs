using Data.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VolunteerModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        [Required]
        public string Reason { get; set; }
        public Organisations[] Organisations { get; set; }
        public Skills[] Skills { get; set; }
    }
}
