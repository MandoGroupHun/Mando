using CSharpFunctionalExtensions;
using MandoWebApp.Models;

public interface ICategoryService
{
    Task<Result> AddCategoryAsync(Category category);
}