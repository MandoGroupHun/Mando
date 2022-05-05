﻿using MandoWebApp.Models;
using MandoWebApp.Services.InviteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize(Roles = $"{Roles.Manager},{Roles.Administrator}")]
[ApiController]
[Route("[controller]")]
public class InviteController : ControllerBase
{
    private readonly ILogger<InviteController> _logger;
    private readonly IInviteService _inviteService;

    public InviteController(ILogger<InviteController> logger, IInviteService inviteService)
    {
        _logger = logger;
        _inviteService = inviteService;
    }

    [HttpGet]
    public Task<List<Invite>> Get()
    {
        return _inviteService.GetPendingInvites(50);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Invite newInvite, [FromQuery] string lang = "hu")
    {
        newInvite.Status = InviteStatus.New;
        newInvite.CreatedAt = DateTime.UtcNow;

        var addResult = await _inviteService.AddInvite(newInvite, lang);

        return addResult.IsSuccess
            ? Ok(addResult.Value)
            : BadRequest(addResult.Error);
    }
}
