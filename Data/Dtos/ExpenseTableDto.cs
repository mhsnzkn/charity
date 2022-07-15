using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Data.Constants;

namespace Data.Dtos
{
    public class ExpenseTableDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseStatus Status { get; set; }
        public string UserName { get; set; }
    }
}
