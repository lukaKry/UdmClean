using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Identity.Models;

namespace UdmClean.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "10",
                    Email = "admin@bla.com",
                    NormalizedEmail = "ADMIN@BLA.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@bla.com",
                    NormalizedUserName = "ADMIN@BLA.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "20",
                    Email = "user@bla.com",
                    NormalizedEmail = "USER@BLA.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user@bla.com",
                    NormalizedUserName = "USER@BLA.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                }
                );
        }
    }
}
