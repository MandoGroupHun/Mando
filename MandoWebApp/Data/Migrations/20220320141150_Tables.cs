using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HU_Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    EN_Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "varchar(50)", nullable: false),
                    HU_Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    EN_Description = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HU_Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    EN_Name = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HU_Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    EN_Name = table.Column<string>(type: "varchar(150)", nullable: true),
                    Category = table.Column<string>(type: "varchar(50)", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    SizeType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Map_Building_Product",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "varchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map_Building_Product", x => new { x.BuildingID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Map_Building_Product_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Map_Building_Product_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "ID", "EN_Name", "HU_Name" },
                values: new object[] { 1, "Piece", "Darab" });

            migrationBuilder.CreateIndex(
                name: "IX_Map_Building_Product_ProductID",
                table: "Map_Building_Product",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitID",
                table: "Product",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Map_Building_Product");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
