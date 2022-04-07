using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MandoWebApp.Models.SeedData
{
    public static class BuildingProductSeed
    {
        public static void Run(ModelBuilder builder)
        {
            builder.Entity<Building>().HasData(new Building
            {
                ID = 1,
                Address1 = "Karácsony Sándor u. 31",
                ENDescription = "Mandak house central building",
                ENName = "Mandak house",
                HUDescription = "Mandák ház központi épület",
                HUName = "Mandák ház",
                Zip = 1086
            });

            builder.Entity<Product>().HasData(new Product
            {
                ID = 1,
                Category = "Higéniai eszköz",
                ENName = "Toothbrush",
                HUName = "Fogkefe",
                SizeType = null,
                UnitID = 1
            });

            builder.Entity<BuildingProduct>().HasData(new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 1,
                Quantity = 5,
                Size = null
            });
        }
    }
}
