using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class AddBuildingProductHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Map_Building_Product_History",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "varchar(3)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecordedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map_Building_Product_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Map_Building_Product_History_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Map_Building_Product_History_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKTej6LYApcw/A9NEBoX9ydCedNrpxO/ZI0R2s9NtCZz9YQAwUR0PVl7rt/gzwPiHw==", "71b34132-e522-4fbc-acdd-28968ede961e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEGwp+kiMipHmTV9tQKYx6x5lP6+lpi0jDYBrP7DeG1VSMIK9seA6vZfAM1dqC3yJ9A==", "b9a7f54b-0527-4eef-b6d2-283fe8cbed84" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKwc5Hxjy+hbIKqZN7nWhf65CHY6tdCboib9P9fUhHFOfi/aWDmhOCNQQ0hGDkSf3w==", "22958c5e-1b36-4244-a7bd-331543682960" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEIL9vLAiqiqLbpIChwN7YIGkB2Wj6IljhQxbLJVjpuEn84VPp4gIhWPOtmxISr6lyA==", "fdf0382b-d07c-440b-b93c-cca109c1a2af" });

            migrationBuilder.CreateIndex(
                name: "IX_Map_Building_Product_History_BuildingID",
                table: "Map_Building_Product_History",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Map_Building_Product_History_ProductID",
                table: "Map_Building_Product_History",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Map_Building_Product_History");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEBBKaVt5t3MJ1jWyvikPIVZDv9AGbrFl0SjLZZwG+Hx5cnS9Pz3b2+vUm+KtA5DGGQ==", "eb45e779-a945-4df1-a4cd-ed95d967d6ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPKWXf3UgFJ8FhXnOvvLoTzrT5Zzg072rbIUxU6hDODQz+tNhpPuVduT+Y2VMgV4BA==", "3386c3da-289d-4b4a-8aa8-27c9af45880c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKftoUUtVt4oSCPYo6GkXwIyuznOIXCaudZCD7HrXGzZXIguUz3kkwD0NOsVJ28Z/Q==", "1266a712-a62a-4c37-a1e8-1de9ba703a7e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHaFoGUqd1Rlmxn3AGDDF4xWRD82nE+NfqwfT62bJ9lCA59Qj7l8+1Z2D1n94BZ9DQ==", "d2c7b9f9-e358-48ff-be52-68807949cdc1" });
        }
    }
}
