using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.Datatable
{
    public class TableResponseDto<T>
    {
        public List<T> Records { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
    }
}
