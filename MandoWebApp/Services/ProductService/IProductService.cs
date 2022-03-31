using CSharpFunctionalExtensions;
using MandoWebApp.Models;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.ProductService
{
    public interface IProductService
    {
        List<ProductModel> GetProducts();
        Task<Result> AddBuildingProduct(BuildingProduct buildingProduct);
    }
}
