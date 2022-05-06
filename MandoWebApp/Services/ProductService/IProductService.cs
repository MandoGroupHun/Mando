using CSharpFunctionalExtensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<List<SupplyModel>> GetSuppliesAsync();
        Task<Result> AddBuildingProduct(BuildingProduct buildingProduct);
        Task<Result> UpdateSupplyAsync(SupplyModel supply);
        Task<List<UnitModel>> GetUnitsAsync();
        Task<Result> AddPendingBuildingProduct(PendingBuildingProduct pendingBuildingProduct);
        Task<List<PendingDonationModel>> GetPendingDonationsAsync();
        Task<int> GetPendingDonationCountAsync();
        Task<Result> AcceptPendingBuildingProduct(long pendingBuildingProductId, Product product, BuildingProduct buildingProduct);
        Task<Result> AcceptPendingBuildingProduct(long pendingBuildingProductId, BuildingProduct buildingProduct);
        Task<Result> DeletePendingBuildingProduct(long pendingBuildingProductId);
        Task<Result> AddProductAndBuildingProduct(Product product, BuildingProduct buildingProduct);
        Task<List<CategoryModel>> GetCategoriesAsync();
    }
}
