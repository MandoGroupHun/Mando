using MandoWebApp.Models;
using MandoWebApp.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryInputModel createCategory)
        {
            var addResult = await _categoryService.AddCategoryAsync(new Category
            {
                ENName = createCategory.ENName,
                HUName = createCategory.HUName
            });

            return addResult.IsSuccess
                ? Ok()
                : BadRequest(addResult.Error);
        }
    }
}