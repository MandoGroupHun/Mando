using System.Security.Claims;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<UserManagementService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public string? GetUserId(ClaimsPrincipal user) =>
            user == null ? null : _userManager.GetUserId(user);

        public async Task<UserManagement> GetUsersAndRoles()
        {
            var units = await _dbContext.UserRoles
                .Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (userRole, role) => 
                    new { role.Name, userRole.UserId })
                .ToListAsync();

            var userRoles = units.GroupBy(ur => ur.UserId)
                .Join(_userManager.Users, ur => ur.Key, r => r.Id, (userRoles, user) => 
                    new UserManagementItem(user.Id, user.UserName, userRoles.Select(ur => ur.Name).ToList()))
                .ToList();

            return new UserManagement(_roleManager.Roles.Select(r => r.Name).ToList().OrderByDescending(x => Roles.Priorities[x]), userRoles.OrderBy(u => u.Name));
        }

        public async Task<Result> UpdateRolesAsync(UserManagementItem updatedUser, int updaterPriority)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(updatedUser.Id);

                if (user == null)
                {
                    return Result.Failure("Invalid user");
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                var newRoles = updatedUser.Roles.Except(currentRoles);
                var rolesToRemove = currentRoles.Except(updatedUser.Roles);

                if (newRoles.Concat(rolesToRemove).Any(role => Roles.Priorities[role] > updaterPriority))
                {
                    return Result.Failure("Insufficient premissions");
                }

                await _userManager.AddToRolesAsync(user, newRoles);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during role update");

                return Result.Failure("Error during role update");
            }

            return Result.Success();
        }
    }
}
