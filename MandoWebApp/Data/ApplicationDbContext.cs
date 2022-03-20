using MandoWebApp.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using System.ComponentModel.DataAnnotations.Schema;

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
        builder.Entity<Building_Product>()
       .HasKey(c => new { c.BuildingID, c.ProductID });
        builder.Entity<Unit>().HasData(new Unit { ID = 1, HU_Name = "Darab", EN_Name = "Piece" });

    }

    public DbSet<Building> Buildings { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Building_Product> Building_Products { get; set; }
    public DbSet<Unit> Units { get; set; }
}
