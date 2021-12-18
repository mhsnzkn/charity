using Business.Base;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
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
        //Task<UserModel> GetModelByIdAsync(int id);
        //Task<DataTableResult> GetForDataTable(AccountParamsDto param);
        Task<VolunteerTableDto> GetTable(Expression<Func<Volunteer, bool>> expression = null);
        Task<VolunteerDto> GetByIdAsync(int id);
        Task<Result> Approve(int id);
        Task<Result> Cancel(int id, string cancellationReason);
    }
}
