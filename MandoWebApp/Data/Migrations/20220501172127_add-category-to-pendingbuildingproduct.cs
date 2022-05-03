using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class addcategorytopendingbuildingproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PendingBuildingProducts",
                newName: "ProductName");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PendingBuildingProducts",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "PendingBuildingProducts");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "PendingBuildingProducts",
                newName: "Name");
        }
    }
}
