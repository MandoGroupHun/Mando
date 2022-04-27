using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.BuildingService
{
    public interface IBuildingService
    {
        Task<List<BuildingModel>> GetBuildingsAsync();
    }
}
