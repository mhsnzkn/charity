using Data;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserDal : RepositoryBase<User, AppDbContext>, IUserDal
    {
        private readonly AppDbContext context;

        public UserDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> GetByMail(string email)
        {
            return await  context.Users.Where(a => a.Email == email).FirstOrDefaultAsync();
        }
        public async Task<bool> IsExistByVolunteerId(int volunteerId)
        {
            return await context.Users.AnyAsync(a => a.VolunteerId == volunteerId);
        }
    }
}
