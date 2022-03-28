using Microsoft.AspNetCore.Identity;

namespace MandoWebApp.Models;

public class ApplicationUser : IdentityUser
{
    public bool? IsTestUser { get; set; }
}
