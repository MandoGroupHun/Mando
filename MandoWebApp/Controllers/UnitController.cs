using MandoWebApp.Models;
using MandoWebApp.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UnitController : ControllerBase
    {
        private readonly ILogger<UnitController> _logger;
        private readonly IUnitService _unitService;
        public UnitController(ILogger<UnitController> logger, IUnitService unitService)
        {
            _logger = logger;
            _unitService = unitService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUnitInputModel createUnit)
        {
            var addResult = await _unitService.AddUnitAsync(new Unit
            {
                ENName = createUnit.ENName,
                HUName = createUnit.HUName
            });

            return addResult.IsSuccess
                ? Ok()
                : BadRequest(addResult.Error);
        }
    }
}