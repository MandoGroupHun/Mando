using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class MoveCategoryToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EN_Category",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HU_Category",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "PendingBuildingProducts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "PendingBuildingProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HU_Name = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EN_Name = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "EN_Name", "HU_Name" },
                values: new object[,]
                {
                    { 1, "Hygiene product", "Higiéniai eszköz" },
                    { 2, "Clothing", "Ruha" },
                    { 3, "Medicine", "Gyógyszer" }
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "CategoryID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "CategoryID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                column: "CategoryID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4,
                column: "CategoryID",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBuildingProducts_CategoryID",
                table: "PendingBuildingProducts",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingBuildingProducts_Categories_CategoryID",
                table: "PendingBuildingProducts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingBuildingProducts_Categories_CategoryID",
                table: "PendingBuildingProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_PendingBuildingProducts_CategoryID",
                table: "PendingBuildingProducts");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "PendingBuildingProducts");

            migrationBuilder.AddColumn<string>(
                name: "EN_Category",
                table: "Product",
                type: "varchar(150)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HU_Category",
                table: "Product",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PendingBuildingProducts",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "EN_Category", "HU_Category" },
                values: new object[] { "Hygiene product", "Higiéniai eszköz" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "EN_Category", "HU_Category" },
                values: new object[] { "Clothing", "Ruha" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "EN_Category", "HU_Category" },
                values: new object[] { "Clothing", "Ruha" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "EN_Category", "HU_Category" },
                values: new object[] { "Medicine", "Gyógyszer" });
        }
    }
}
