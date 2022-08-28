using Data;
using Data.Entities;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class AgreementDalUnitTests
    {
        private readonly AppDbContext context;
        public AgreementDalUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test");
            context = new AppDbContext(optionsBuilder.Options);
        }
    }
}
