using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class AddPendingBuildingProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PendingBuildingProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SizeType = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<string>(type: "varchar(3)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsProcessed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBuildingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingBuildingProducts_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingBuildingProducts_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAyvMr+N7jkRvq0UdCJUCoLcuGH1oD5xik3cAie3Ct+ynF9TVzyJ/u+eLwSt6tYQQA==", "3a18f1d3-a88c-450c-9848-9f2599b217ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEBrIYboCCqymLtKItwhyXIsA92k7ZOHVUFx0mEuPaAntA/+3tY7DmtpxXpM7zh+JZQ==", "8230f0a6-9dbf-499e-859c-846b5cadce86" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJ0OY9PiS6r4074ZgIRj2So/XNp4eiboZG5edPrg3EtW7BpgzTTQpoDz+Q2zICI86A==", "c4fb04c2-336f-4d37-b224-53fd9a4d34b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEC5Y+qfYeedruyLBh9OHGlrBaBfGXtfPJDLFmDgX7au39ma+uwKrSFIzFoHUYff0dg==", "61455e2a-6e6a-405c-99ab-d2245999c21a" });

            migrationBuilder.CreateIndex(
                name: "IX_PendingBuildingProducts_BuildingID",
                table: "PendingBuildingProducts",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBuildingProducts_UnitID",
                table: "PendingBuildingProducts",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingBuildingProducts");

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
        }
    }
}
