using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.Datatable
{
    public class TableBaseDto<T>
    {
        public List<T> Records { get; set; }
        public int Total { get; set; }
    }
}
