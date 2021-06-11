using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgGoogleMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMap",
                table: "STUDENTS");

            migrationBuilder.AddColumn<string>(
                name: "GoogleMap",
                table: "SCHOOLS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMap",
                table: "SCHOOLS");

            migrationBuilder.AddColumn<string>(
                name: "GoogleMap",
                table: "STUDENTS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
