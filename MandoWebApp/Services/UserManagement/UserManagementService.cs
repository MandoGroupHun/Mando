using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MandoWebApp.Services.UserManangement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<UserManagementService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<UserManagementItem>> GetUsersAsync()
        {
            var units = await _dbContext.UserRoles
                .Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (userRole, role) => new { role.Name, userRole.UserId }).ToListAsync();

            return units.GroupBy(ur => ur.UserId)
                .Join(_userManager.Users, ur => ur.Key, r => r.Id, (userRoles, user) => new UserManagementItem(user.Id, user.UserName, userRoles.Select(ur => ur.Name)))
                .ToList();
        }

        public Task<Result> UpdateRolesAsync(UserManagementItem updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
