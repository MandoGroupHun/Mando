using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class ModifyPendingBuildingProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "PendingBuildingProducts",
                newName: "HU_ProductName");

            migrationBuilder.AddColumn<string>(
                name: "EN_ProductName",
                table: "PendingBuildingProducts",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "PendingBuildingProducts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedByUserId",
                table: "PendingBuildingProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EN_ProductName",
                table: "PendingBuildingProducts");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "PendingBuildingProducts");

            migrationBuilder.DropColumn(
                name: "ProcessedByUserId",
                table: "PendingBuildingProducts");

            migrationBuilder.RenameColumn(
                name: "HU_ProductName",
                table: "PendingBuildingProducts",
                newName: "ProductName");
        }
    }
}
