using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.BuildingService
{
    public interface IBuildingService
    {
        List<BuildingModel> GetBuildings();
    }
}
