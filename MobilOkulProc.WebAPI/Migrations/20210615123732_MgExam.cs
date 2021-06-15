using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassSectionsID",
                table: "EXAMS",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EXAMS_ClassSectionsID",
                table: "EXAMS",
                column: "ClassSectionsID");

            migrationBuilder.CreateIndex(
                name: "IX_CLASS_SECTIONS_CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS",
                column: "CLASS_SECTIONObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_CLASS_SECTIONS_CLASS_SECTIONS_CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS",
                column: "CLASS_SECTIONObjectID",
                principalTable: "CLASS_SECTIONS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EXAMS_CLASS_SECTIONS_ClassSectionsID",
                table: "EXAMS",
                column: "ClassSectionsID",
                principalTable: "CLASS_SECTIONS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLASS_SECTIONS_CLASS_SECTIONS_CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_EXAMS_CLASS_SECTIONS_ClassSectionsID",
                table: "EXAMS");

            migrationBuilder.DropIndex(
                name: "IX_EXAMS_ClassSectionsID",
                table: "EXAMS");

            migrationBuilder.DropIndex(
                name: "IX_CLASS_SECTIONS_CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS");

            migrationBuilder.DropColumn(
                name: "ClassSectionsID",
                table: "EXAMS");

            migrationBuilder.DropColumn(
                name: "CLASS_SECTIONObjectID",
                table: "CLASS_SECTIONS");
        }
    }
}
