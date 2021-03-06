// <auto-generated />
using System;
using MandoWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220427153148_Localize-Product-Category")]
    partial class LocalizeProductCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Use")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("MandoWebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsTestUser")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "42a760de-497b-44c1-84f0-9388087fc344",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "336f6c3e-701a-4382-a61e-760f0e3f01d2",
                            Email = "volunteer@mandakdb.com",
                            EmailConfirmed = true,
                            IsTestUser = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "VOLUNTEER@MANDAKDB.COM",
                            NormalizedUserName = "VOLUNTEER@MANDAKDB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEOVp1FXc36/EfcHxhWlNRLjsbYsnygLP9uwkIMfgxotN6BMUQ/wHShPAvLxfUeHBUA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8b48510b-7531-4ad3-926b-79fb2232a31d",
                            TwoFactorEnabled = false,
                            UserName = "volunteer@mandakdb.com"
                        },
                        new
                        {
                            Id = "ff732cde-8b20-46b3-a698-2da238d80c2d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d4d63954-822f-44d7-8f5f-dd5754947767",
                            Email = "benefactor@mandakdb.com",
                            EmailConfirmed = true,
                            IsTestUser = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "BENEFACTOR@MANDAKDB.COM",
                            NormalizedUserName = "BENEFACTOR@MANDAKDB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEOfNObILHtTGCFPMvlA7BOzkTp0AEy7XaBHUuflOD70zSQvMOHXsDkSPlMOVrGj8WA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d416df10-7743-4b49-8aea-d80c0ee25bbd",
                            TwoFactorEnabled = false,
                            UserName = "benefactor@mandakdb.com"
                        },
                        new
                        {
                            Id = "98195404-8874-4ecd-a28e-cba611fa7f88",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7985c0e2-6c28-44fc-b303-198e354abee5",
                            Email = "manager@mandakdb.com",
                            EmailConfirmed = true,
                            IsTestUser = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "MANAGER@MANDAKDB.COM",
                            NormalizedUserName = "MANAGER@MANDAKDB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAENc5zbeexqV304YKw1ryce4iAYSo+IluYjqDszvE8EuCumajfnyIWYRVVzaQQ+Y4mw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "62af57b1-9beb-43c2-a426-65079d42a684",
                            TwoFactorEnabled = false,
                            UserName = "manager@mandakdb.com"
                        },
                        new
                        {
                            Id = "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "98450eec-b7da-4e55-a044-1e02f5dc270d",
                            Email = "administrator@mandakdb.com",
                            EmailConfirmed = true,
                            IsTestUser = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINISTRATOR@MANDAKDB.COM",
                            NormalizedUserName = "ADMINISTRATOR@MANDAKDB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEIlCepNbdtAy5rmJLOIHsGX3Z15ezfKjIUVf0w0/kw257Cd77pb4+QasaIJr7O6i/Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e60c4ad9-44d5-4238-94ca-ac0903ff8ba8",
                            TwoFactorEnabled = false,
                            UserName = "administrator@mandakdb.com"
                        });
                });

            modelBuilder.Entity("MandoWebApp.Models.Building", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ENDescription")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("EN_Description");

                    b.Property<string>("ENName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("EN_Name");

                    b.Property<string>("HUDescription")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("HU_Description");

                    b.Property<string>("HUName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("HU_Name");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Building");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Address1 = "Karácsony Sándor u. 31",
                            ENDescription = "Mandak house central building",
                            ENName = "Mandak house",
                            HUDescription = "Mandák ház központi épület",
                            HUName = "Mandák ház",
                            Zip = 1086
                        });
                });

            modelBuilder.Entity("MandoWebApp.Models.BuildingProduct", b =>
                {
                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("varchar(3)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BuildingID", "ProductID", "Size");

                    b.HasIndex("ProductID");

                    b.ToTable("Map_Building_Product");

                    b.HasData(
                        new
                        {
                            BuildingID = 1,
                            ProductID = 1,
                            Size = "",
                            Quantity = 5
                        },
                        new
                        {
                            BuildingID = 1,
                            ProductID = 2,
                            Size = "S",
                            Quantity = 3
                        },
                        new
                        {
                            BuildingID = 1,
                            ProductID = 3,
                            Size = "42",
                            Quantity = 2
                        },
                        new
                        {
                            BuildingID = 1,
                            ProductID = 3,
                            Size = "44",
                            Quantity = 2
                        },
                        new
                        {
                            BuildingID = 1,
                            ProductID = 4,
                            Size = "",
                            Quantity = 10
                        });
                });

            modelBuilder.Entity("MandoWebApp.Models.Invite", b =>
                {
                    b.Property<Guid>("InviteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("InviteId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Invite");
                });

            modelBuilder.Entity("MandoWebApp.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ENCategory")
                        .HasColumnType("varchar(150)")
                        .HasColumnName("EN_Category");

                    b.Property<string>("ENName")
                        .HasColumnType("varchar(150)")
                        .HasColumnName("EN_Name");

                    b.Property<string>("HUCategory")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnName("HU_Category");

                    b.Property<string>("HUName")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnName("HU_Name");

                    b.Property<int?>("SizeType")
                        .HasColumnType("int");

                    b.Property<int>("UnitID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UnitID");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ENCategory = "Hygiene product",
                            ENName = "Toothbrush",
                            HUCategory = "Higiéniai eszköz",
                            HUName = "Fogkefe",
                            UnitID = 2
                        },
                        new
                        {
                            ID = 2,
                            ENCategory = "Clothing",
                            ENName = "Female shirt",
                            HUCategory = "Ruha",
                            HUName = "Női ing",
                            SizeType = 1,
                            UnitID = 1
                        },
                        new
                        {
                            ID = 3,
                            ENCategory = "Clothing",
                            ENName = "Male shirt",
                            HUCategory = "Ruha",
                            HUName = "Férfi ing",
                            SizeType = 0,
                            UnitID = 1
                        },
                        new
                        {
                            ID = 4,
                            ENCategory = "Medicine",
                            ENName = "Painkiller",
                            HUCategory = "Gyógyszer",
                            HUName = "Fájdalomcsillapító",
                            UnitID = 2
                        });
                });

            modelBuilder.Entity("MandoWebApp.Models.Unit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ENName")
                        .HasColumnType("varchar(20)")
                        .HasColumnName("EN_Name");

                    b.Property<string>("HUName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("HU_Name");

                    b.HasKey("ID");

                    b.ToTable("Unit");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ENName = "piece",
                            HUName = "darab"
                        },
                        new
                        {
                            ID = 2,
                            ENName = "box",
                            HUName = "doboz"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "99ed77a4-097d-4175-9ff0-85e583eabe89",
                            ConcurrencyStamp = "e7438c3f-c644-4fcf-aebb-f66c123345ae",
                            Name = "Volunteer",
                            NormalizedName = "VOLUNTEER"
                        },
                        new
                        {
                            Id = "8a8969ac-7762-4637-98bc-228471a240fb",
                            ConcurrencyStamp = "776ae477-4a75-4aaa-8cf7-24f472ad374c",
                            Name = "Benefactor",
                            NormalizedName = "BENEFACTOR"
                        },
                        new
                        {
                            Id = "45115a8a-11f6-4192-83d2-f04b3e3fd0bb",
                            ConcurrencyStamp = "6923d565-7496-4c75-91d9-848aed4c7ffd",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "6439468e-28bc-4d50-bf26-e4e3bb93bbbb",
                            ConcurrencyStamp = "b925cfd5-3307-4c02-9182-d6a3dda4709c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "42a760de-497b-44c1-84f0-9388087fc344",
                            RoleId = "99ed77a4-097d-4175-9ff0-85e583eabe89"
                        },
                        new
                        {
                            UserId = "ff732cde-8b20-46b3-a698-2da238d80c2d",
                            RoleId = "8a8969ac-7762-4637-98bc-228471a240fb"
                        },
                        new
                        {
                            UserId = "98195404-8874-4ecd-a28e-cba611fa7f88",
                            RoleId = "45115a8a-11f6-4192-83d2-f04b3e3fd0bb"
                        },
                        new
                        {
                            UserId = "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                            RoleId = "6439468e-28bc-4d50-bf26-e4e3bb93bbbb"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MandoWebApp.Models.BuildingProduct", b =>
                {
                    b.HasOne("MandoWebApp.Models.Building", null)
                        .WithMany("BuildingProducts")
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MandoWebApp.Models.Product", null)
                        .WithMany("BuildingProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MandoWebApp.Models.Product", b =>
                {
                    b.HasOne("MandoWebApp.Models.Unit", null)
                        .WithMany("Products")
                        .HasForeignKey("UnitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MandoWebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MandoWebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MandoWebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MandoWebApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MandoWebApp.Models.Building", b =>
                {
                    b.Navigation("BuildingProducts");
                });

            modelBuilder.Entity("MandoWebApp.Models.Product", b =>
                {
                    b.Navigation("BuildingProducts");
                });

            modelBuilder.Entity("MandoWebApp.Models.Unit", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
