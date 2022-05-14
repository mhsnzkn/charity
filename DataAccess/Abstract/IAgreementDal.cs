using Data.Entities;
using Data.Models;
using DataAccess.Base;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAgreementDal : IRepositoryBase<Agreement>
    {
        Task<AgreementModel> GetModelById(int id);
    }
}
