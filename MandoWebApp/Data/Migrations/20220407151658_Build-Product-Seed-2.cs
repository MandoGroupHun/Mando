using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class BuildProductSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Map_Building_Product",
                table: "Map_Building_Product");

            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Map_Building_Product",
                type: "varchar(3)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Map_Building_Product",
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID", "Size" });

            migrationBuilder.InsertData(
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID", "Size", "Quantity" },
                values: new object[] { 1, 1, "", 5 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Category", "EN_Name", "HU_Name", "SizeType", "UnitID" },
                values: new object[,]
                {
                    { 2, "Ruha", "Female shirt", "Női ing", 1, 1 },
                    { 3, "Ruha", "Male shirt", "Férfi ing", 0, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Unit",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "EN_Name", "HU_Name" },
                values: new object[] { "piece", "darab" });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "ID", "EN_Name", "HU_Name" },
                values: new object[] { 2, "box", "doboz" });

            migrationBuilder.InsertData(
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID", "Size", "Quantity" },
                values: new object[,]
                {
                    { 1, 2, "S", 3 },
                    { 1, 3, "42", 2 },
                    { 1, 3, "44", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "UnitID",
                value: 2);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Category", "EN_Name", "HU_Name", "SizeType", "UnitID" },
                values: new object[] { 4, "Gyógyszer", "Painkiller", "Fájdalomcsillapító", null, 2 });

            migrationBuilder.InsertData(
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID", "Size", "Quantity" },
                values: new object[] { 1, 4, "", 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Map_Building_Product",
                table: "Map_Building_Product");

            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID", "Size" },
                keyValues: new object[] { 1, 2, "S" });

            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID", "Size" },
                keyValues: new object[] { 1, 3, "42" });

            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID", "Size" },
                keyValues: new object[] { 1, 3, "44" });

            migrationBuilder.DeleteData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID", "Size" },
                keyValues: new object[] { 1, 4, "" });

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Unit",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Map_Building_Product",
                type: "varchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Map_Building_Product",
                table: "Map_Building_Product",
                columns: new[] { "BuildingID", "ProductID" });

            migrationBuilder.UpdateData(
                table: "Map_Building_Product",
                keyColumns: new[] { "BuildingID", "ProductID" },
                keyValues: new object[] { 1, 1 },
                column: "Size",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "UnitID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Unit",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "EN_Name", "HU_Name" },
                values: new object[] { "Piece", "Darab" });
        }
    }
}
