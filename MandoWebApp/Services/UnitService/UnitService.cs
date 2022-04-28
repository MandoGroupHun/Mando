using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;

namespace MandoWebApp.Services.UnitService
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UnitService> _logger;

        public UnitService(ApplicationDbContext dbContext, ILogger<UnitService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result> AddUnitAsync(Unit unit)
        {
            try 
            {
                var existingUnit = _dbContext.Units.FirstOrDefault(x => x.HUName == unit.HUName || x.ENName == unit.ENName);

                if (existingUnit is not null)
                {
                    return Result.Failure("The unit already exists.");
                }

                await _dbContext.AddAsync(unit);
                await _dbContext.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation of new unit");

                return Result.Failure("Error during bulding product creation");
            }

            return Result.Success();
        }
    }
}