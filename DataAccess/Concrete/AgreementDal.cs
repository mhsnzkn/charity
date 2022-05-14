using Data;
using Data.Entities;
using Data.Models;
using DataAccess.Abstract;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class AgreementDal : RepositoryBase<Agreement, AppDbContext>, IAgreementDal
    {
        private readonly AppDbContext context;

        public AgreementDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<AgreementModel> GetModelById(int id)
        {
            var q = context.Agreements.Where(x => x.Id == id);
            var w = q.Select(x => new AgreementModel
            {
                Id = x.Id,
                Content = x.Content,
                IsActive = x.IsActive,
                Order = x.Order,
                Title = x.Title,
                InUse = x.VolunteerAgreements.Count > 0,
            });
            return await w.FirstOrDefaultAsync();
        }
    }
}
