using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgNewDbTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SCHOOL_STUDENT_SCHOOLS_SchoolID",
                table: "SCHOOL_STUDENT");

            migrationBuilder.DropForeignKey(
                name: "FK_SCHOOL_STUDENT_STUDENTS_StudentID",
                table: "SCHOOL_STUDENT");

            migrationBuilder.DropForeignKey(
                name: "FK_TEACHER_SCHOOL_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOL");

            migrationBuilder.DropForeignKey(
                name: "FK_TEACHER_SCHOOL_TEACHERS_TeacherID",
                table: "TEACHER_SCHOOL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEACHER_SCHOOL",
                table: "TEACHER_SCHOOL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SCHOOL_STUDENT",
                table: "SCHOOL_STUDENT");

            migrationBuilder.RenameTable(
                name: "TEACHER_SCHOOL",
                newName: "TEACHER_SCHOOLS");

            migrationBuilder.RenameTable(
                name: "SCHOOL_STUDENT",
                newName: "SCHOOL_STUDENTS");

            migrationBuilder.RenameIndex(
                name: "IX_TEACHER_SCHOOL_TeacherID",
                table: "TEACHER_SCHOOLS",
                newName: "IX_TEACHER_SCHOOLS_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_TEACHER_SCHOOL_SchoolID",
                table: "TEACHER_SCHOOLS",
                newName: "IX_TEACHER_SCHOOLS_SchoolID");

            migrationBuilder.RenameIndex(
                name: "IX_SCHOOL_STUDENT_StudentID",
                table: "SCHOOL_STUDENTS",
                newName: "IX_SCHOOL_STUDENTS_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_SCHOOL_STUDENT_SchoolID",
                table: "SCHOOL_STUDENTS",
                newName: "IX_SCHOOL_STUDENTS_SchoolID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEACHER_SCHOOLS",
                table: "TEACHER_SCHOOLS",
                column: "ObjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SCHOOL_STUDENTS",
                table: "SCHOOL_STUDENTS",
                column: "ObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_SCHOOL_STUDENTS_SCHOOLS_SchoolID",
                table: "SCHOOL_STUDENTS",
                column: "SchoolID",
                principalTable: "SCHOOLS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SCHOOL_STUDENTS_STUDENTS_StudentID",
                table: "SCHOOL_STUDENTS",
                column: "StudentID",
                principalTable: "STUDENTS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEACHER_SCHOOLS_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOLS",
                column: "SchoolID",
                principalTable: "SCHOOLS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEACHER_SCHOOLS_TEACHERS_TeacherID",
                table: "TEACHER_SCHOOLS",
                column: "TeacherID",
                principalTable: "TEACHERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SCHOOL_STUDENTS_SCHOOLS_SchoolID",
                table: "SCHOOL_STUDENTS");

            migrationBuilder.DropForeignKey(
                name: "FK_SCHOOL_STUDENTS_STUDENTS_StudentID",
                table: "SCHOOL_STUDENTS");

            migrationBuilder.DropForeignKey(
                name: "FK_TEACHER_SCHOOLS_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOLS");

            migrationBuilder.DropForeignKey(
                name: "FK_TEACHER_SCHOOLS_TEACHERS_TeacherID",
                table: "TEACHER_SCHOOLS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEACHER_SCHOOLS",
                table: "TEACHER_SCHOOLS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SCHOOL_STUDENTS",
                table: "SCHOOL_STUDENTS");

            migrationBuilder.RenameTable(
                name: "TEACHER_SCHOOLS",
                newName: "TEACHER_SCHOOL");

            migrationBuilder.RenameTable(
                name: "SCHOOL_STUDENTS",
                newName: "SCHOOL_STUDENT");

            migrationBuilder.RenameIndex(
                name: "IX_TEACHER_SCHOOLS_TeacherID",
                table: "TEACHER_SCHOOL",
                newName: "IX_TEACHER_SCHOOL_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_TEACHER_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOL",
                newName: "IX_TEACHER_SCHOOL_SchoolID");

            migrationBuilder.RenameIndex(
                name: "IX_SCHOOL_STUDENTS_StudentID",
                table: "SCHOOL_STUDENT",
                newName: "IX_SCHOOL_STUDENT_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_SCHOOL_STUDENTS_SchoolID",
                table: "SCHOOL_STUDENT",
                newName: "IX_SCHOOL_STUDENT_SchoolID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEACHER_SCHOOL",
                table: "TEACHER_SCHOOL",
                column: "ObjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SCHOOL_STUDENT",
                table: "SCHOOL_STUDENT",
                column: "ObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_SCHOOL_STUDENT_SCHOOLS_SchoolID",
                table: "SCHOOL_STUDENT",
                column: "SchoolID",
                principalTable: "SCHOOLS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SCHOOL_STUDENT_STUDENTS_StudentID",
                table: "SCHOOL_STUDENT",
                column: "StudentID",
                principalTable: "STUDENTS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEACHER_SCHOOL_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOL",
                column: "SchoolID",
                principalTable: "SCHOOLS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEACHER_SCHOOL_TEACHERS_TeacherID",
                table: "TEACHER_SCHOOL",
                column: "TeacherID",
                principalTable: "TEACHERS",
                principalColumn: "ObjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
