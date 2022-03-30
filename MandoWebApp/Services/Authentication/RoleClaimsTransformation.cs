using MandoWebApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MandoWebApp.Services.Authentication
{
    public class RoleClaimsTransformation : IClaimsTransformation
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleClaimsTransformation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var userId = (principal.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return principal;
            }

            var roles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (_, role) => role.Name).ToListAsync();

            var newClaims = roles.Where(role => !principal.IsInRole(role)).Select(role => new Claim(Roles.ClaimType, role));

            if (newClaims.Any())
            {
                principal.AddIdentity(new ClaimsIdentity(newClaims, authenticationType: default, nameType: default, roleType: Roles.ClaimType));
            }

            return principal;
        }
    }
}
