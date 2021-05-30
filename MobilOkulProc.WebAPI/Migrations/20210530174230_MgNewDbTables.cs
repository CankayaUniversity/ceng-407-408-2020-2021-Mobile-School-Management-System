using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgNewDbTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCHOOL_STUDENT",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    SchoolID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHOOL_STUDENT", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SCHOOL_STUDENT_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHOOL_STUDENT_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEACHER_SCHOOL",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    TeacherID = table.Column<int>(nullable: false),
                    SchoolID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEACHER_SCHOOL", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_TEACHER_SCHOOL_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEACHER_SCHOOL_TEACHERS_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "TEACHERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_STUDENT_SchoolID",
                table: "SCHOOL_STUDENT",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_STUDENT_StudentID",
                table: "SCHOOL_STUDENT",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOL_SchoolID",
                table: "TEACHER_SCHOOL",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOL_TeacherID",
                table: "TEACHER_SCHOOL",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SCHOOL_STUDENT");

            migrationBuilder.DropTable(
                name: "TEACHER_SCHOOL");
        }
    }
}
