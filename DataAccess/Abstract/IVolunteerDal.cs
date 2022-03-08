using Data.Entities;
using Data.Utility.Results;
using DataAccess.Base;
using System;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IVolunteerDal : IRepositoryBase<Volunteer>
    {
        Task<Result> Cancel(Volunteer volunteer, string cancellationReason);
        Task<Volunteer> GetByKey(Guid key);
    }
}
