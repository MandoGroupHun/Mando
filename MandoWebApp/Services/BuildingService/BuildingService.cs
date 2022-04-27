using MandoWebApp.Data;
using MandoWebApp.Extensions;
using MandoWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MandoWebApp.Services.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BuildingService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BuildingModel>> GetBuildingsAsync()
        {
            var units = await _dbContext.Units.ToListAsync();
            var lang = _httpContextAccessor.HttpContext?.GetLang()!;

            var buildings = await _dbContext.Buildings.ToListAsync();

            return buildings.Select(x => new BuildingModel
            {
                BuildingId = x.ID,
                Name = x.Name(lang),
                Zip = x.Zip,
                Description = x.Description(lang),
                Address = x.Address1
            }).ToList();
        }
    }
}
