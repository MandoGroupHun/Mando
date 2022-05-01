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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEG6e5wIvhQALrP1BeqOAkkhK+b+x4k78oIF19Y2AYiCKFqeN1tbnO+9vedUunuoK8A==", "f2905677-fcba-4453-9b4c-426149ebfb23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPv69ui5KCE7Ia7WgKwVQ+s/Rzl3/DT1R38XsiWTVPVbuL83RfpaiDUMmop0blyS6g==", "17400527-65b0-4d96-a278-88a88c1572fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEB43qov9KEFcrbBrdVTWv+SI5bAxnEE6+F49g9LwJ1DqifIt8BOQyRjBZgTTUdaCyw==", "ac8e3805-83ab-4535-a9c0-c54c2f67d06a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEEjoM4Mr8fduUyyqJc6GMPzTlEq6jT5SRq/oLsDTMOh2+olujRVEN3lpCm8OefL8KQ==", "fc8d83de-3031-4fc0-8f64-6e46bdcf4d3d" });
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
        }
    }
}
