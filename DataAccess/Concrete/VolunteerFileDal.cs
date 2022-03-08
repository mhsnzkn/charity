using Data;
using Data.Entities;
using Data.Utility.Results;
using DataAccess.Abstract;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class VolunteerFileDal : RepositoryBase<VolunteerFile, AppDbContext>, IVolunteerFileDal
    {
        private readonly AppDbContext context;

        public VolunteerFileDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
