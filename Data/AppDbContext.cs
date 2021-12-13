using Data.Entities;
using Data.Utility.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Constants.Constants;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var password = "H4R.admin*";
            SecurityHelper.CreatePasswordHash(password, out var passwordHash, out var salt);
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Email = "admin@heart4refugees.org",
                    Name = "Admin",
                    Role = UserRoles.Admin,
                    Status = Constants.Enums.UserStatus.Active,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt
                });
        }

    }
}
