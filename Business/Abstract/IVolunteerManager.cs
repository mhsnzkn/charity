using Business.Utility;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVolunteerManager
    {
        //Task<User> GetByIdAsync(int id);
        //Task<UserModel> GetModelByIdAsync(int id);
        //Task<List<User>> Get(Expression<Func<User, bool>> expression = null);
        //Task<DataTableResult> GetForDataTable(AccountParamsDto param);

        Task<Result> Add(VolunteerModel model);
        //Task<Result> Update(UserModel model);
        //Task<Result> Delete(User entity);
    }
}
