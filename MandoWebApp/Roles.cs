using IdentityModel;
using System.Security.Claims;

namespace MandoWebApp;
public static class Roles
{
    public const string Volunteer = "Volunteer";            // Önkéntes
    public const string Benefactor = "Benefactor";          // Fogadóhely képviselő
    public const string Manager = "Manager";                // Munkavezető
    public const string Administrator = "Administrator";    // Admin

    public static Dictionary<string, int> Priorities => new()
    {
        { "Benefactor", 5},
        { "Volunteer", 10},
        { "Manager", 15},
        { "Administrator", 20},
    };

    public static int GetHighestRole(this ClaimsPrincipal user) => user.Claims.Where(c => c.Type == JwtClaimTypes.Role).Select(role => Priorities[role.Value]).Max();
}
