using CSharpFunctionalExtensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.UserManangement
{
    public interface IUserManagementService
    {
        Task<UserManagement> GetUsersAndRoles();
        Task<Result> UpdateRolesAsync(UserManagementItem updatedUser);
    }
}
