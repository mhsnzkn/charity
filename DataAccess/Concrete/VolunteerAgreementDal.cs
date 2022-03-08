using Data;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Base;

namespace DataAccess.Concrete
{
    public class VolunteerAgreementDal : RepositoryBase<VolunteerAgreement, AppDbContext>, IVolunteerAgreementDal
    {
        public VolunteerAgreementDal(AppDbContext context) : base(context)
        {
        }
    }
}
