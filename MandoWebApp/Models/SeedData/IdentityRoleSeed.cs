using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MandoWebApp.Models.SeedData
{
    public static class IdentityRoleSeed
    {
        public static void Run(ModelBuilder builder)
        {
            var roles = new (string Name, string RoleId, string UserId, string RoleConcurrencyStamp, string UserConnurrencyStamp)[] {
                ("Volunteer", "99ed77a4-097d-4175-9ff0-85e583eabe89", "42a760de-497b-44c1-84f0-9388087fc344", "e7438c3f-c644-4fcf-aebb-f66c123345ae", "336f6c3e-701a-4382-a61e-760f0e3f01d2"),        // Önkéntes
                ("Benefactor", "8a8969ac-7762-4637-98bc-228471a240fb", "ff732cde-8b20-46b3-a698-2da238d80c2d", "776ae477-4a75-4aaa-8cf7-24f472ad374c", "d4d63954-822f-44d7-8f5f-dd5754947767"),       // Fogadóhely képviselő
                ("Manager", "45115a8a-11f6-4192-83d2-f04b3e3fd0bb", "98195404-8874-4ecd-a28e-cba611fa7f88", "6923d565-7496-4c75-91d9-848aed4c7ffd", "7985c0e2-6c28-44fc-b303-198e354abee5"),          // Munkavezető
                ("Administrator", "6439468e-28bc-4d50-bf26-e4e3bb93bbbb", "8d644e3b-3e19-446b-a45a-301a3a0144b7", "b925cfd5-3307-4c02-9182-d6a3dda4709c", "98450eec-b7da-4e55-a044-1e02f5dc270d"),    // Admin
            };

            builder.Entity<IdentityRole>().HasData(roles.Select(role =>
                new IdentityRole<string>
                {
                    Id = role.RoleId,
                    Name = role.Name,
                    NormalizedName = role.Name.ToUpper(),
                    ConcurrencyStamp = role.RoleConcurrencyStamp,
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
                    ConcurrencyStamp = role.UserConnurrencyStamp,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    IsTestUser = true
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
