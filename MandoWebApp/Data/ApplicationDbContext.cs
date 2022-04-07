using MandoWebApp.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using MandoWebApp.Models.SeedData;

namespace MandoWebApp.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BuildingProduct>()
            .HasKey(c => new { c.BuildingID, c.ProductID });

        builder.Entity<Unit>().HasData(new { ID = 1, HUName = "Darab", ENName = "Piece" });

        builder.Entity<Invite>().HasIndex(i => i.Email).IsUnique();

        IdentityRoleSeed.Run(builder);
        BuildingProductSeed.Run(builder);
    }

    public DbSet<Building> Buildings { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<BuildingProduct> BuildingProducts { get; set; }
    public DbSet<Unit> Units { get; set; }
}
