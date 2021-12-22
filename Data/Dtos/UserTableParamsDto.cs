using Data.Dtos.Datatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Constants.Enums;

namespace Data.Dtos
{
    public class UserTableParamsDto : TableParams
    {
        public UserStatus Status { get; set; }
    }
}
