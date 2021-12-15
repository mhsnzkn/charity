using Business.Base;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVolunteerManager : IBaseManager<Volunteer, VolunteerModel>
    {
        //Task<User> GetByIdAsync(int id);
        //Task<UserModel> GetModelByIdAsync(int id);
        Task<VolunteerTableDto> GetTable(Expression<Func<Volunteer, bool>> expression = null);
        //Task<DataTableResult> GetForDataTable(AccountParamsDto param);

    }
}
