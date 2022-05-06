using System.Security.Claims;
using CSharpFunctionalExtensions;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.UserManangement
{
    public interface IUserManagementService
    {
        Task<UserManagement> GetUsersAndRoles();
        Task<Result> UpdateRolesAsync(UserManagementItem updatedUser, int updaterPriority);
        string? GetUserId(ClaimsPrincipal user);
    }
}
