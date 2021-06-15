using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgLectureandGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LECTURES_STUDENTS_StudentsID",
                table: "LECTURES");

            migrationBuilder.DropIndex(
                name: "IX_LECTURES_StudentsID",
                table: "LECTURES");

            migrationBuilder.DropColumn(
                name: "StudentsID",
                table: "LECTURES");

            migrationBuilder.AddColumn<int>(
                name: "ClassSectionsID",
                table: "LECTURES",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "GRADES",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_ClassSectionsID",
                table: "LECTURES",
                column: "ClassSectionsID");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_StudentID",
                table: "GRADES",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRADES_STUDENTS_StudentID",
                table: "GRADES",
                column: "StudentID",
                principalTable: "STUDENTS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LECTURES_CLASS_SECTIONS_ClassSectionsID",
                table: "LECTURES",
                column: "ClassSectionsID",
                principalTable: "CLASS_SECTIONS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRADES_STUDENTS_StudentID",
                table: "GRADES");

            migrationBuilder.DropForeignKey(
                name: "FK_LECTURES_CLASS_SECTIONS_ClassSectionsID",
                table: "LECTURES");

            migrationBuilder.DropIndex(
                name: "IX_LECTURES_ClassSectionsID",
                table: "LECTURES");

            migrationBuilder.DropIndex(
                name: "IX_GRADES_StudentID",
                table: "GRADES");

            migrationBuilder.DropColumn(
                name: "ClassSectionsID",
                table: "LECTURES");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "GRADES");

            migrationBuilder.AddColumn<int>(
                name: "StudentsID",
                table: "LECTURES",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_StudentsID",
                table: "LECTURES",
                column: "StudentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_LECTURES_STUDENTS_StudentsID",
                table: "LECTURES",
                column: "StudentsID",
                principalTable: "STUDENTS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
