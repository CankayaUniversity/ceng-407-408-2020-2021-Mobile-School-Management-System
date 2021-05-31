using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class MgLectureGradeSyllabusDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DAYSES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    DayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DAYSES", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "LECTURES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    LectureName = table.Column<string>(maxLength: 100, nullable: false),
                    StudentObjectID = table.Column<int>(nullable: true),
                    TeacherObjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LECTURES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_LECTURES_STUDENTS_StudentObjectID",
                        column: x => x.StudentObjectID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LECTURES_TEACHERS_TeacherObjectID",
                        column: x => x.TeacherObjectID,
                        principalTable: "TEACHERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCHOOL_STUDENTS",
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
                    table.PrimaryKey("PK_SCHOOL_STUDENTS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SCHOOL_STUDENTS_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHOOL_STUDENTS_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEACHER_SCHOOLS",
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
                    table.PrimaryKey("PK_TEACHER_SCHOOLS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_TEACHER_SCHOOLS_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEACHER_SCHOOLS_TEACHERS_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "TEACHERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRADES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Grade = table.Column<double>(nullable: false),
                    LectureObjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_GRADES_LECTURES_LectureObjectID",
                        column: x => x.LectureObjectID,
                        principalTable: "LECTURES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYLLABUSES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LectureObjectID = table.Column<int>(nullable: true),
                    DaysObjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYLLABUSES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SYLLABUSES_DAYSES_DaysObjectID",
                        column: x => x.DaysObjectID,
                        principalTable: "DAYSES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYLLABUSES_LECTURES_LectureObjectID",
                        column: x => x.LectureObjectID,
                        principalTable: "LECTURES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_LectureObjectID",
                table: "GRADES",
                column: "LectureObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_StudentObjectID",
                table: "LECTURES",
                column: "StudentObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_TeacherObjectID",
                table: "LECTURES",
                column: "TeacherObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_STUDENTS_SchoolID",
                table: "SCHOOL_STUDENTS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_STUDENTS_StudentID",
                table: "SCHOOL_STUDENTS",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_SYLLABUSES_DaysObjectID",
                table: "SYLLABUSES",
                column: "DaysObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SYLLABUSES_LectureObjectID",
                table: "SYLLABUSES",
                column: "LectureObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOLS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOLS_TeacherID",
                table: "TEACHER_SCHOOLS",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRADES");

            migrationBuilder.DropTable(
                name: "SCHOOL_STUDENTS");

            migrationBuilder.DropTable(
                name: "SYLLABUSES");

            migrationBuilder.DropTable(
                name: "TEACHER_SCHOOLS");

            migrationBuilder.DropTable(
                name: "DAYSES");

            migrationBuilder.DropTable(
                name: "LECTURES");
        }
    }
}
