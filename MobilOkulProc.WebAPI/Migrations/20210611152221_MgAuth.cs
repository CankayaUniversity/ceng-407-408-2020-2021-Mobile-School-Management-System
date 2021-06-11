using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "zUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "zUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "zUsers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "zUsers");
        }
    }
}
