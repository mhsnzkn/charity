using Data.Abstract;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Expense : Entity
    {
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        [StringLength(100)]
        public string ModeOfTransport { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalMileage { get; set; }
        [StringLength(100)]
        public string Claim { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PayDate { get; set; }
        public CommonFile CommonFile { get; set; }
        public int? CommonFileId { get; set; }
    }
}
