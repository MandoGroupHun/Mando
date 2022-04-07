using MandoWebApp.Models.ViewModels;
using MandoWebApp.Services.UserManangement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = $"{Roles.Manager},{Roles.Administrator}")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        public Task<UserManagement> Get()
        {
            return _userManagementService.GetUsersAndRoles();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserManagementItem user)
        {
            var updaterPriority = HttpContext.User.GetHighestRole();

            var updateResult = await _userManagementService.UpdateRolesAsync(user, updaterPriority);

            return updateResult.IsSuccess
                ? Ok()
                : BadRequest(updateResult.Error);
        }
    }
}
