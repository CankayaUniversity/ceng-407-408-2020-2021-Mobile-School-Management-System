using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class Mg_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_ReceiveID",
                table: "MESSAGES",
                column: "ReceiveID");

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES",
                column: "ReceiveID",
                principalTable: "USERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES");

            migrationBuilder.DropIndex(
                name: "IX_MESSAGES_ReceiveID",
                table: "MESSAGES");
        }
    }
}
