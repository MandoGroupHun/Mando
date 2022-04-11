using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext dbContext, ILogger<ProductService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public List<ProductModel> GetProducts()
        {
            var units = _dbContext.Units.ToList();

            return _dbContext.Products.ToList().Select(x => new ProductModel
            {
                ProductId = x.ID,
                Name = x.Name,
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

        public List<SupplyModel> GetSupplies()
        {
            var units = _dbContext.Units.ToList();

            var supplies = _dbContext.Products.Join(_dbContext.BuildingProducts, p => p.ID, bp => bp.ProductID, (product, buildingProduct) => new
            {
                product.ID,
                product.Name,
                product.UnitID,
                product.Category,
                buildingProduct.Quantity,
                buildingProduct.Size
            });

            return supplies.ToList().Select(x => new SupplyModel
            {
                ProductId = x.ID,
                Name = x.Name,
                UnitName = units.First(u => u.ID == x.UnitID).Name,
                Category = x.Category,
                Size = x.Size ?? string.Empty,
                Quantity = x.Quantity
            }).ToList();
        }
    }
}
