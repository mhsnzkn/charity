using Data;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Data.Utility.Results;
using DataAccess.Abstract;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class VolunteerDal : RepositoryBase<Volunteer, AppDbContext>, IVolunteerDal
    {
        private readonly AppDbContext context;

        public VolunteerDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Volunteer> GetByKey(Guid key)
        {
            return await context.Volunteers.Where(a=>a.Key == key).FirstOrDefaultAsync();
        }

        public async Task<Result> Cancel(Volunteer volunteer, string cancellationReason)
        {
            var result = new Result();
            try
            {
                volunteer.Status = VolunteerStatus.Cancelled;
                volunteer.CancellationReason = cancellationReason;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<List<DropDownItem>> GetForDropDown()
        {
            return await context.Volunteers.Where(x => x.Status == VolunteerStatus.Completed).OrderBy(a=>a.FirstName).Select(a => new DropDownItem
            {
                Id = a.Id.ToString(), 
                Name = a.FirstName+ a.LastName
            }).ToListAsync();
        }
    }
}
