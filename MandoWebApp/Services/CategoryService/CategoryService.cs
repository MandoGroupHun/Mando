using CSharpFunctionalExtensions;
using MandoWebApp.Data;
using MandoWebApp.Models;

namespace MandoWebApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ApplicationDbContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result> AddCategoryAsync(Category category)
        {
            try 
            {
                var existingCategory = _dbContext.Categories.FirstOrDefault(x => x.HUName == category.HUName || x.ENName == category.ENName);

                if (existingCategory is not null)
                {
                    return Result.Failure("The unit already exists.");
                }

                await _dbContext.AddAsync(category);
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