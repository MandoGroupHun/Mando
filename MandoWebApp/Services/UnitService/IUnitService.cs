using CSharpFunctionalExtensions;
using MandoWebApp.Models;

public interface IUnitService
{
    Task<Result> AddUnit(Unit unit);
}