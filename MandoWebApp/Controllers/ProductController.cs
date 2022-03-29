using MandoWebApp.Models;
using MandoWebApp.Models.Input;
using MandoWebApp.Models.ViewModels;
using MandoWebApp.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public List<ProductModel> Get()
    {
        return _productService.GetProducts();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBuildingProductInputModel createBuildingProduct)
    {
        var addResult = await _productService.AddBuildingProduct(new BuildingProduct
        {
            BuildingID = 1, // TODO
            ProductID = createBuildingProduct.ProductID,
            Quantity = createBuildingProduct.Quantity,
            Size = !string.IsNullOrWhiteSpace(createBuildingProduct.Size) ? createBuildingProduct.Size : null,
        });

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(addResult.Error);
    }
}
