using CSharpFunctionalExtensions;
using MandoWebApp.Models;

public interface IUnitService
{
    Task<Result> AddUnitAsync(Unit unit);
}