using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class IdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45115a8a-11f6-4192-83d2-f04b3e3fd0bb", "6923d565-7496-4c75-91d9-848aed4c7ffd", "Manager", "MANAGER" },
                    { "6439468e-28bc-4d50-bf26-e4e3bb93bbbb", "b925cfd5-3307-4c02-9182-d6a3dda4709c", "Administrator", "ADMINISTRATOR" },
                    { "8a8969ac-7762-4637-98bc-228471a240fb", "776ae477-4a75-4aaa-8cf7-24f472ad374c", "Benefactor", "BENEFACTOR" },
                    { "99ed77a4-097d-4175-9ff0-85e583eabe89", "e7438c3f-c644-4fcf-aebb-f66c123345ae", "Volunteer", "VOLUNTEER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "42a760de-497b-44c1-84f0-9388087fc344", 0, "336f6c3e-701a-4382-a61e-760f0e3f01d2", "volunteer@mandakdb.com", true, false, null, "VOLUNTEER@MANDAKDB.COM", "VOLUNTEER@MANDAKDB.COM", "AQAAAAEAACcQAAAAEICpam3OOmFBYpJiqNXwaYXnOD29EdoAmjQTAP8iOKaxg2nTz7OaaccygRlrqiAQcw==", null, false, "50e16ca3-7dc4-46d6-9242-448e1b0a2ee1", false, "volunteer@mandakdb.com" },
                    { "8d644e3b-3e19-446b-a45a-301a3a0144b7", 0, "98450eec-b7da-4e55-a044-1e02f5dc270d", "administrator@mandakdb.com", true, false, null, "ADMINISTRATOR@MANDAKDB.COM", "ADMINISTRATOR@MANDAKDB.COM", "AQAAAAEAACcQAAAAEELs55iWWIqAooS8Xv9nWuKaDy0Db7JTS9Iql50wyIzpVXHTjRJeG/1m/sP61TGGCQ==", null, false, "3ee1cf25-fbd6-4a1b-a031-98d3dca6dfe0", false, "administrator@mandakdb.com" },
                    { "98195404-8874-4ecd-a28e-cba611fa7f88", 0, "7985c0e2-6c28-44fc-b303-198e354abee5", "manager@mandakdb.com", true, false, null, "MANAGER@MANDAKDB.COM", "MANAGER@MANDAKDB.COM", "AQAAAAEAACcQAAAAECuyQcv9iaySxL1DfqC6kXr0Q+DWPtufMuNKd96XLH0s79Vj6SAC/jK68JTllgFt6w==", null, false, "691b8102-1322-4705-a644-4a58a47d1a8a", false, "manager@mandakdb.com" },
                    { "ff732cde-8b20-46b3-a698-2da238d80c2d", 0, "d4d63954-822f-44d7-8f5f-dd5754947767", "benefactor@mandakdb.com", true, false, null, "BENEFACTOR@MANDAKDB.COM", "BENEFACTOR@MANDAKDB.COM", "AQAAAAEAACcQAAAAEKJ1Q4J6v8QE40AxwmfcqxsWdme1IGWLnzVEGlGxQf7plQcy8j8IpB2e/qijivWymw==", null, false, "3071699d-2ee8-4677-8396-f98394704909", false, "benefactor@mandakdb.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "99ed77a4-097d-4175-9ff0-85e583eabe89", "42a760de-497b-44c1-84f0-9388087fc344" },
                    { "6439468e-28bc-4d50-bf26-e4e3bb93bbbb", "8d644e3b-3e19-446b-a45a-301a3a0144b7" },
                    { "45115a8a-11f6-4192-83d2-f04b3e3fd0bb", "98195404-8874-4ecd-a28e-cba611fa7f88" },
                    { "8a8969ac-7762-4637-98bc-228471a240fb", "ff732cde-8b20-46b3-a698-2da238d80c2d" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "99ed77a4-097d-4175-9ff0-85e583eabe89", "42a760de-497b-44c1-84f0-9388087fc344" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6439468e-28bc-4d50-bf26-e4e3bb93bbbb", "8d644e3b-3e19-446b-a45a-301a3a0144b7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45115a8a-11f6-4192-83d2-f04b3e3fd0bb", "98195404-8874-4ecd-a28e-cba611fa7f88" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8a8969ac-7762-4637-98bc-228471a240fb", "ff732cde-8b20-46b3-a698-2da238d80c2d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45115a8a-11f6-4192-83d2-f04b3e3fd0bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6439468e-28bc-4d50-bf26-e4e3bb93bbbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a8969ac-7762-4637-98bc-228471a240fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99ed77a4-097d-4175-9ff0-85e583eabe89");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42a760de-497b-44c1-84f0-9388087fc344");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d644e3b-3e19-446b-a45a-301a3a0144b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98195404-8874-4ecd-a28e-cba611fa7f88");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff732cde-8b20-46b3-a698-2da238d80c2d");
        }
    }
}
