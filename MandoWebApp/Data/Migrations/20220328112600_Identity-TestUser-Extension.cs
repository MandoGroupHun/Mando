using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class IdentityTestUserExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTestUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "IsTestUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "AQAAAAEAACcQAAAAEDzZLBzI03iuMONiGUrZxAG9l89XzAp5MPKbzA1Piix1XPKRlC4x+5ZlcHFyTjXjsg==", "af93a8f2-c0ba-4b44-a43a-d65bc43fd608" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "IsTestUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "AQAAAAEAACcQAAAAEIUcZIQ4XkTiSq+l/DVMxIQ9lhMYWse7x3VVZkBbY4WHdkwzkiIlAxfd4bDXvyrQVg==", "f4e33c59-e1da-49ea-8a8d-3fb9f4178494" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "IsTestUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "AQAAAAEAACcQAAAAEM/smygeqGZ/qxatd1FAEpSgSRa5rvFAhzfXc1A1lSSpkhym2om2kxePJYXb1njFMg==", "f5f059a2-c6fc-4a33-852a-24a94d20c58e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "IsTestUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { true, "AQAAAAEAACcQAAAAEGehDWrCpXSzn8caWMHcZMWJQ7hm2lewcwpUR5Ay2n15H7s+Rgn3hp2Wp4N8kI19CA==", "cf38b9ff-68f3-4257-b129-d14cbb362544" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTestUser",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEICpam3OOmFBYpJiqNXwaYXnOD29EdoAmjQTAP8iOKaxg2nTz7OaaccygRlrqiAQcw==", "50e16ca3-7dc4-46d6-9242-448e1b0a2ee1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEELs55iWWIqAooS8Xv9nWuKaDy0Db7JTS9Iql50wyIzpVXHTjRJeG/1m/sP61TGGCQ==", "3ee1cf25-fbd6-4a1b-a031-98d3dca6dfe0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAECuyQcv9iaySxL1DfqC6kXr0Q+DWPtufMuNKd96XLH0s79Vj6SAC/jK68JTllgFt6w==", "691b8102-1322-4705-a644-4a58a47d1a8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKJ1Q4J6v8QE40AxwmfcqxsWdme1IGWLnzVEGlGxQf7plQcy8j8IpB2e/qijivWymw==", "3071699d-2ee8-4677-8396-f98394704909" });
        }
    }
}
