using CSharpFunctionalExtensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.UserManangement
{
    public interface IUserManagementService
    {
        Task<List<UserManagementItem>> GetUsersAsync();
        Task<Result> UpdateRolesAsync(UserManagementItem updatedUser);
    }
}
