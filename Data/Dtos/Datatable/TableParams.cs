using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.Datatable
{
    public class TableParams
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string SearchString { get; set; }
    }
}
