using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandoWebApp.Data.Migrations
{
    public partial class AddInviteStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Invite",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invite");
        }
    }
}
