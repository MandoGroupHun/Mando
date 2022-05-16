using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class MoveSizeTypeToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeType",
                table: "Product",
                newName: "SizeTypeID");

            migrationBuilder.RenameColumn(
                name: "SizeType",
                table: "PendingBuildingProducts",
                newName: "SizeTypeID");

            migrationBuilder.CreateTable(
                name: "SizeTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HU_Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EN_Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Examples = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeTypes", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SizeTypes",
                columns: new[] { "ID", "EN_Name", "Examples", "HU_Name" },
                values: new object[,]
                {
                    { 1, "Number", "32, 36, 44", "Szám" },
                    { 2, "Character", "S, M, L, XL", "Betű" },
                    { 3, "Child", "126, 134", "Gyerek" }
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "SizeTypeID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                column: "SizeTypeID",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Product_SizeTypeID",
                table: "Product",
                column: "SizeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBuildingProducts_SizeTypeID",
                table: "PendingBuildingProducts",
                column: "SizeTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingBuildingProducts_SizeTypes_SizeTypeID",
                table: "PendingBuildingProducts",
                column: "SizeTypeID",
                principalTable: "SizeTypes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SizeTypes_SizeTypeID",
                table: "Product",
                column: "SizeTypeID",
                principalTable: "SizeTypes",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingBuildingProducts_SizeTypes_SizeTypeID",
                table: "PendingBuildingProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SizeTypes_SizeTypeID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "SizeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Product_SizeTypeID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_PendingBuildingProducts_SizeTypeID",
                table: "PendingBuildingProducts");

            migrationBuilder.RenameColumn(
                name: "SizeTypeID",
                table: "Product",
                newName: "SizeType");

            migrationBuilder.RenameColumn(
                name: "SizeTypeID",
                table: "PendingBuildingProducts",
                newName: "SizeType");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "SizeType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                column: "SizeType",
                value: 0);
        }
    }
}
