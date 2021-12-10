using Data;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class VolunteerDal : RepositoryBase<Volunteer, AppDbContext>, IVolunteerDal
    {
        public VolunteerDal(AppDbContext context) : base(context)
        {
        }
    }
}
