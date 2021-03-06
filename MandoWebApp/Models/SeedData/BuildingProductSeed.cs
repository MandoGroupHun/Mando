using Microsoft.EntityFrameworkCore;

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

            builder.Entity<Category>().HasData(new Category
            {
                ID = 1,
                HUName = "Higiéniai eszköz",
                ENName = "Hygiene product"
            },
            new Category
            {
                ID = 2,
                HUName = "Ruha",
                ENName = "Clothing"
            },
            new Category
            {
                ID = 3,
                HUName = "Gyógyszer",
                ENName = "Medicine"
            });

            builder.Entity<Product>().HasData(new Product
            {
                ID = 1,
                CategoryID = 1,
                ENName = "Toothbrush",
                HUName = "Fogkefe",
                SizeTypeID = null,
                UnitID = 2
            },
            new Product
            {
                ID = 2,
                CategoryID = 2,
                ENName = "Female shirt",
                HUName = "Női ing",
                SizeTypeID = 2,
                UnitID = 1
            },
            new Product
            {
                ID = 3,
                CategoryID = 2,
                ENName = "Male shirt",
                HUName = "Férfi ing",
                SizeTypeID = 1,
                UnitID = 1
            },
            new Product
            {
                ID = 4,
                CategoryID = 3,
                ENName = "Painkiller",
                HUName = "Fájdalomcsillapító",
                SizeTypeID = null,
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

            builder.Entity<SizeType>().HasData(new SizeType
            {
                ID = 1,
                HUName = "Szám",
                ENName = "Number",
                Examples = "32, 36, 44"
            },
            new SizeType
            {
                ID = 2,
                HUName = "Betű",
                ENName = "Character",
                Examples = "S, M, L, XL"
            },
            new SizeType
            {
                ID = 3,
                HUName = "Gyerek",
                ENName = "Child",
                Examples = "126, 134"
            });
        }
    }
}
