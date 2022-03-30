using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize]
[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    public RoleController()
    {
    }

    [HttpGet("own")]
    public List<string> Own()
    {
        return User.Claims.Where(c => c.Type == Roles.ClaimType).Select(c => c.Value).ToList();
    }
}
