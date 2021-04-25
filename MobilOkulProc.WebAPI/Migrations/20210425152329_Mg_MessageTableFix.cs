using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class Mg_MessageTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_SenderID",
                table: "MESSAGES",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES",
                column: "ReceiveID",
                principalTable: "USERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGES_USERS_SenderID",
                table: "MESSAGES",
                column: "SenderID",
                principalTable: "USERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES");

            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGES_USERS_SenderID",
                table: "MESSAGES");

            migrationBuilder.DropIndex(
                name: "IX_MESSAGES_SenderID",
                table: "MESSAGES");

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGES_USERS_ReceiveID",
                table: "MESSAGES",
                column: "ReceiveID",
                principalTable: "USERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
