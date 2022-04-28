using MandoWebApp.Models.ViewModels;
using MandoWebApp.Services.BuildingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class BuildingController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    public async Task<List<BuildingModel>> Buildings()
    {
        return await _buildingService.GetBuildingsAsync();
    }
}
