using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class BuildProductSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "ID", "Address1", "EN_Description", "EN_Name", "HU_Description", "HU_Name", "Zip" },
                values: new object[] { 1, "Karácsony Sándor u. 31", "Mandak house central building", "Mandak house", "Mandák ház központi épület", "Mandák ház", 1086 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Category", "EN_Name", "HU_Name", "SizeType", "UnitID" },
                values: new object[] { 1, "Higéniai eszköz", "Toothbrush", "Fogkefe", null, 1 });

            migrationBuilder.InsertData(
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID", "Quantity", "Size" },
                values: new object[] { 1, 1, 5, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
