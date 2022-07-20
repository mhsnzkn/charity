using Data.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ExpenseModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        [StringLength(100)]
        [Required]
        public string ModeOfTransport { get; set; }
        public decimal TotalMileage { get; set; }
        [StringLength(100)]
        public string Claim { get; set; }
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        public IFormFile FormFile { get; set; }
        public string FilePath { get; set; }

        public bool IsValid()
        {
            if(this.Amount == 0)
                return false;

            return true;
        }
    }
}
