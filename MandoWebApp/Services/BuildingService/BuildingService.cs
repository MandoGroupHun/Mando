using MandoWebApp.Data;
using MandoWebApp.Models.ViewModels;

namespace MandoWebApp.Services.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly ApplicationDbContext _dbContext;

        public BuildingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BuildingModel> GetBuildings()
        {
            var units = _dbContext.Units.ToList();

            return _dbContext.Buildings.ToList().Select(x => new BuildingModel
            {
                BuildingId = x.ID,
                Name = x.Name,
                Zip = x.Zip,
                Description = x.Description,
                Address = x.Address1
            }).ToList();
        }
    }
}
