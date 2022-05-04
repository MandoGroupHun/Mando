using CSharpFunctionalExtensions;
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
            HuProductName = createPendingBuildingProduct.HuProductName,
            EnProductName = createPendingBuildingProduct.EnProductName,
            CategoryID = createPendingBuildingProduct.CategoryId,
            Quantity = createPendingBuildingProduct.Quantity,
            SizeType = createPendingBuildingProduct.SizeType,
            Size = !string.IsNullOrWhiteSpace(createPendingBuildingProduct.Size) ? createPendingBuildingProduct.Size : string.Empty,
            UnitID = createPendingBuildingProduct.UnitId
        });

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(addResult.Error);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptPendingBuildingProduct([FromBody] AcceptPendingBuildingProductInputModel acceptPendingBuildingProduct)
    {
        var buildingProduct = new BuildingProduct
        {
            BuildingID = acceptPendingBuildingProduct.BuildingId,
            Quantity = acceptPendingBuildingProduct.Quantity,
            Size = !string.IsNullOrWhiteSpace(acceptPendingBuildingProduct.Size) ? acceptPendingBuildingProduct.Size : string.Empty,
        };

        Result addResult;
        if (acceptPendingBuildingProduct.ProductId.HasValue)
        {
            buildingProduct.ProductID = acceptPendingBuildingProduct.ProductId.Value;
            addResult = await _productService.AcceptPendingBuildingProduct(acceptPendingBuildingProduct.PendingBuildingProductId, buildingProduct);
        }
        else
        {
            var product = new Product
            {
                HUName = acceptPendingBuildingProduct.HuProductName,
                ENName = acceptPendingBuildingProduct.EnProductName,
                CategoryID = acceptPendingBuildingProduct.CategoryId,
                UnitID = acceptPendingBuildingProduct.UnitId,
                SizeType = acceptPendingBuildingProduct.SizeType
            };

            addResult = await _productService.AcceptPendingBuildingProduct(acceptPendingBuildingProduct.PendingBuildingProductId, product, buildingProduct);
        }

        return addResult.IsSuccess
            ? Ok()
            : BadRequest(addResult.Error);
    }

    [HttpDelete]
    public async Task<IActionResult> DeclinePendingBuildingProduct([FromQuery] long pendingBuildingProductId)
    {
        var deleteResult = await _productService.DeletePendingBuildingProduct(pendingBuildingProductId);

        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.Error);
    }

    [HttpGet]
    public Task<List<PendingDonationModel>> PendingDonations()
    {
        return _productService.GetPendingDonationsAsync();
    }

    [HttpGet]
    public Task<int> PendingDonationCount()
    {
        return _productService.GetPendingDonationCountAsync();
    }

    [HttpGet]
    public Task<List<CategoryModel>> Categories()
    {
        return _productService.GetCategoriesAsync();
    }
}
