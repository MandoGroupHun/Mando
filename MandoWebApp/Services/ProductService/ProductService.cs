using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Extensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;
using MandoWebApp.Services.UserManangement;
using Microsoft.EntityFrameworkCore;

namespace MandoWebApp.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ProductService> _logger;
        private readonly IUserManagementService _userManagementService;

        public ProductService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, IUserManagementService userManagementService, ILogger<ProductService> logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManagementService = userManagementService;
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var products = await _dbContext.Products.ToListAsync();
            var lang = _httpContextAccessor.HttpContext?.GetLang()!;

            return products.Select(x => new ProductModel
            {
                ProductId = x.ID,
                Name = x.Name(lang),
                UnitName = units.First(u => u.ID == x.UnitID).Name(lang),
                Category = x.Category(lang),
                SizeType = x.SizeType
            }).ToList();
        }

        public async Task<List<UnitModel>> GetUnitsAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var lang = _httpContextAccessor.HttpContext?.GetLang()!;

            return units.Select(x => new UnitModel
            {
                UnitId = x.ID,
                Name = x.Name(lang)
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

                _dbContext.Add(new BuildingProductHistory
                {
                    BuildingID = buildingProduct.BuildingID,
                    ProductID = buildingProduct.ProductID,
                    Size = buildingProduct.Size,
                    Quantity = buildingProduct.Quantity,
                    RecordedAt = DateTime.UtcNow,
                    UserId = _userManagementService.GetUserId(_httpContextAccessor.HttpContext?.User)
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation of new bulding product");

                return Result.Failure("Error during bulding product creation");
            }

            return Result.Success();
        }

        public async Task<Result> AddPendingBuildingProduct(PendingBuildingProduct pendingBuildingProduct)
        {
            try
            {
                pendingBuildingProduct.RecordedAt = DateTime.UtcNow;
                pendingBuildingProduct.UserId = _userManagementService.GetUserId(_httpContextAccessor.HttpContext?.User);

                _dbContext.Add(pendingBuildingProduct);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation of new pending bulding product");

                return Result.Failure("Error during pending bulding product creation");
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
                Category = lang == "en" && product.ENCategory != null ? product.ENCategory : product.HUCategory,
                buildingProduct.Quantity,
                buildingProduct.Size
            }).ToListAsync();

            return supplies.Select(x => new SupplyModel
            {
                ProductId = x.ID,
                Name = x.Name,
                UnitName = units.First(u => u.ID == x.UnitID).Name(lang),
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

        public async Task<List<PendingDonationModel>> GetPendingDonationsAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var pendingBuildingProducts = await _dbContext.PendingBuildingProducts.Where(x => !x.IsProcessed).ToListAsync();
            var lang = _httpContextAccessor.HttpContext?.GetLang()!;
            var users = await _userManagementService.GetUsersAndRoles();

            return pendingBuildingProducts.Select(x => new PendingDonationModel
            {
                ProductName = x.ProductName,
                UnitName = units.First(u => u.ID == x.UnitID).Name(lang),
                Category = x.Category,
                SizeType = x.SizeType,
                Size = x.Size,
                RecordedAt = x.RecordedAt,
                Quantity = x.Quantity,
                UserName = users.Users.FirstOrDefault(u => u.Id == x.UserId)?.Name
            }).ToList();
        }
    }
}
