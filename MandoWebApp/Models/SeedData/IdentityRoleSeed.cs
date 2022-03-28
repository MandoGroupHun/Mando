using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MandoWebApp.Models.SeedData
{
    public static class IdentityRoleSeed
    {
        public static void Run(ModelBuilder builder)
        {
            var roles = new (string Name, string RoleId, string UserId)[] {
                ("Volunteer", "99ed77a4-097d-4175-9ff0-85e583eabe89", "42a760de-497b-44c1-84f0-9388087fc344"),        // Önkéntes
                ("Benefactor", "8a8969ac-7762-4637-98bc-228471a240fb", "ff732cde-8b20-46b3-a698-2da238d80c2d"),       // Fogadóhely képviselő
                ("Manager", "45115a8a-11f6-4192-83d2-f04b3e3fd0bb", "98195404-8874-4ecd-a28e-cba611fa7f88"),          // Munkavezető
                ("Administrator", "6439468e-28bc-4d50-bf26-e4e3bb93bbbb", "8d644e3b-3e19-446b-a45a-301a3a0144b7"),    // Admin
            };

            builder.Entity<IdentityRole>().HasData(roles.Select(role =>
                new IdentityRole<string>
                {
                    Id = role.RoleId,
                    Name = role.Name,
                    NormalizedName = role.Name.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                })
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(roles.Select(role =>
            {
                var email = $"{role.Name.ToLower()}@mandakdb.com";
                var user = new ApplicationUser
                {
                    Id = role.UserId,
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true
                };

                user.PasswordHash = hasher.HashPassword(user, email);

                return user;
            }));

            builder.Entity<IdentityUserRole<string>>().HasData(roles.Select(role =>
                new IdentityUserRole<string>
                {
                    RoleId = role.RoleId,
                    UserId = role.UserId
                })
            );
        }
    }
}
