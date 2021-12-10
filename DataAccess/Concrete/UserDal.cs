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
    public class UserDal : RepositoryBase<User, AppDbContext>, IUserDal
    {
        public UserDal(AppDbContext context) : base(context)
        {
        }
    }
}
