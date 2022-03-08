using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class CommonFile : Entity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(500)]
        public string Path { get; set; }
    }
}
