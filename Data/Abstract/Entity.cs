using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime? UptDate { get; set; }
    }
}
