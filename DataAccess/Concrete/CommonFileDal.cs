using Data;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Base;

namespace DataAccess.Concrete
{
    public class CommonFileDal : RepositoryBase<CommonFile, AppDbContext>, ICommonFileDal
    {
        public CommonFileDal(AppDbContext context) : base(context)
        {
        }
    }
}
