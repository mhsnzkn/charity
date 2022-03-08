using Data;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Base;

namespace DataAccess.Concrete
{
    public class AgreementDal : RepositoryBase<Agreement, AppDbContext>, IAgreementDal
    {
        public AgreementDal(AppDbContext context) : base(context)
        {
        }
    }
}
