using MandoWebApp.Models;
using MandoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize]
[ApiController]
[Route("invites")]
public class InviteController : ControllerBase
{
    private readonly ILogger<InviteController> _logger;
    private readonly IInviteManager _inviteService;

    public InviteController(ILogger<InviteController> logger, IInviteManager inviteService)
    {
        _logger = logger;
        _inviteService = inviteService;
    }

    [HttpGet]
    public Task<List<Invite>> Get()
    {
        return _inviteService.GetPendingInvites(50);
    }
}
