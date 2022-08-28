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

    }
}
