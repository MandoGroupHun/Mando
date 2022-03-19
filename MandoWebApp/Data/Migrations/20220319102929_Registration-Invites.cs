using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class RegistrationInvites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invite",
                columns: table => new
                {
                    InviteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invite", x => x.InviteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invite");
        }
    }
}
