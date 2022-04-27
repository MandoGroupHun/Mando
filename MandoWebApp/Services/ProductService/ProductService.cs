using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Extensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MandoWebApp.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, ILogger<ProductService> logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var products = await _dbContext.Products.ToListAsync();
                
            return products.Select(x => new ProductModel
            {
                ProductId = x.ID,
                Name = x.Name(_httpContextAccessor.HttpContext?.GetLang()!),
                UnitName = units.First(u => u.ID == x.UnitID).Name,
                Category = x.Category,
                SizeType = x.SizeType
            }).ToList();
        }

        public async Task<Result> AddBuildingProduct(BuildingProduct buildingProduct)
        {
            try
            {
                var existingProduct = _dbContext.BuildingProducts.FirstOrDefault(x => x.BuildingID == buildingProduct.BuildingID &&
                    x.ProductID == buildingProduct.ProductID &&
                    x.Size == buildingProduct.Size);

                if (existingProduct == null)
                {
                    _dbContext.Add(buildingProduct);
                }
                else
                {
                    existingProduct.Quantity += buildingProduct.Quantity;
                    _dbContext.Update(existingProduct);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation of new bulding product");

                return Result.Failure("Error during bulding product creation");
            }

            return Result.Success();
        }

        public async Task<List<SupplyModel>> GetSuppliesAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var lang = _httpContextAccessor.HttpContext?.GetLang()!;

            var supplies = await _dbContext.Products.Join(_dbContext.BuildingProducts, p => p.ID, bp => bp.ProductID, (product, buildingProduct) => new
            {
                product.ID,
                Name = lang == "en" && product.ENName != null ? product.ENName : product.HUName,
                product.UnitID,
                product.Category,
                buildingProduct.Quantity,
                buildingProduct.Size
            }).ToListAsync();

            return supplies.Select(x => new SupplyModel
            {
                ProductId = x.ID,
                Name = x.Name,
                UnitName = units.First(u => u.ID == x.UnitID).Name,
                Category = x.Category,
                Size = x.Size ?? string.Empty,
                Quantity = x.Quantity
            }).ToList();
        }

        public async Task<Result> UpdateSupplyAsync(SupplyModel supply)
        {
            try
            {
                var storedSupply = await _dbContext.BuildingProducts.AsTracking()
                    .FirstAsync(bp => bp.ProductID == supply.ProductId && bp.Size == supply.Size);

                storedSupply.Quantity = supply.Quantity;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during quantity update of building product");

                return Result.Failure("Error during update");
            }

            return Result.Success();
        }
    }
}
