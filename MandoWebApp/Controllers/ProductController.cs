using MandoWebApp.Models;
using MandoWebApp.Models.Input;
using MandoWebApp.Models.ViewModels;
using MandoWebApp.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MandoWebApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
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
    public Task<List<ProductModel>> Products()
    {
        return _productService.GetProductsAsync();
    }

    [HttpGet]
    public Task<List<UnitModel>> Units()
    {
        return _productService.GetUnitsAsync();
    }

    [HttpGet]
    public Task<List<SupplyModel>> Supplies()
    {
        return _productService.GetSuppliesAsync();
    }

    [HttpPost]
    public async Task<IActionResult> SupplyUpdate(SupplyModel supply)
    {
        var updateResult = await _productService.UpdateSupplyAsync(supply);

        return updateResult.IsSuccess
            ? Ok()
            : BadRequest(updateResult.Error);
    }

    [HttpPost]
    public async Task<IActionResult> AddBuildingProduct([FromBody] CreateBuildingProductInputModel createBuildingProduct)
    {
        var addResult = await _productService.AddBuildingProduct(new BuildingProduct
        {
            BuildingID = createBuildingProduct.BuildingId,
            ProductID = createBuildingProduct.ProductId,
            Quantity = createBuildingProduct.Quantity,
            Size = !string.IsNullOrWhiteSpace(createBuildingProduct.Size) ? createBuildingProduct.Size : string.Empty,
        });

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(addResult.Error);
    }

    [HttpPost]
    public async Task<IActionResult> AddPendingBuildingProduct([FromBody] CreatePendingBuildingProductInputModel createPendingBuildingProduct)
    {
        var addResult = await _productService.AddPendingBuildingProduct(new PendingBuildingProduct
        {
            BuildingID = createPendingBuildingProduct.BuildingId,
            ProductName = createPendingBuildingProduct.ProductName,
            Category = createPendingBuildingProduct.Category,
            Quantity = createPendingBuildingProduct.Quantity,
            SizeType = createPendingBuildingProduct.SizeType,
            Size = !string.IsNullOrWhiteSpace(createPendingBuildingProduct.Size) ? createPendingBuildingProduct.Size : string.Empty,
            UnitID = createPendingBuildingProduct.UnitId
        });

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(addResult.Error);
    }
}
