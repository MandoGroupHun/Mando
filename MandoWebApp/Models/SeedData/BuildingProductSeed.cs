using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MandoWebApp.Models.SeedData
{
    public static class BuildingProductSeed
    {
        public static void Run(ModelBuilder builder)
        {
            builder.Entity<Unit>().HasData(
                new Unit { ID = 1, HUName = "darab", ENName = "piece" },
                new Unit { ID = 2, HUName = "doboz", ENName = "box" }
                );

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
                UnitID = 2
            },
            new Product
            {
                ID = 2,
                Category = "Ruha",
                ENName = "Female shirt",
                HUName = "Női ing",
                SizeType = SizeType.TShirt,
                UnitID = 1
            },
            new Product
            {
                ID = 3,
                Category = "Ruha",
                ENName = "Male shirt",
                HUName = "Férfi ing",
                SizeType = SizeType.Numbered,
                UnitID = 1
            },
            new Product
            {
                ID = 4,
                Category = "Gyógyszer",
                ENName = "Painkiller",
                HUName = "Fájdalomcsillapító",
                SizeType = null,
                UnitID = 2
            });

            builder.Entity<BuildingProduct>().HasData(new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 1,
                Quantity = 5,
                Size = string.Empty
            }, new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 2,
                Quantity = 3,
                Size = "S"
            }, new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 3,
                Quantity = 2,
                Size = "42"
            }, new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 3,
                Quantity = 2,
                Size = "44"
            }, new BuildingProduct
            {
                BuildingID = 1,
                ProductID = 4,
                Quantity = 10,
                Size = string.Empty
            });
        }
    }
}
