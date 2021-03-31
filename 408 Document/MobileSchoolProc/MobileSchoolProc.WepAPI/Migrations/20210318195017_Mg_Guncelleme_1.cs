using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class Mg_Guncelleme_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BRANCHS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    BranchName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANCHS", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "CITYS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    CityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITYS", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "EDUCATIONAL_TERMS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    EducationTerm = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDUCATIONAL_TERMS", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE_TYPES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    EmployeeType = table.Column<string>(maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE_TYPES", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    NameSurname = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "EDUCATIONAL_INSTITUTIONs",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    EducationalName = table.Column<string>(maxLength: 50, nullable: false),
                    Logo = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Adres = table.Column<string>(maxLength: 300, nullable: true),
                    CityID = table.Column<int>(nullable: false),
                    WebSite = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDUCATIONAL_INSTITUTIONs", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_EDUCATIONAL_INSTITUTIONs_CITYS_CityID",
                        column: x => x.CityID,
                        principalTable: "CITYS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FEEDBACKs",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    FeedbackContent = table.Column<string>(maxLength: 500, nullable: false),
                    FeedbackType = table.Column<int>(nullable: false),
                    FeedbackDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEEDBACKs", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_FEEDBACKs_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    PriorityID = table.Column<int>(nullable: false),
                    MessageTitle = table.Column<string>(maxLength: 50, nullable: true),
                    MessageContent = table.Column<string>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: false),
                    ReadTime = table.Column<DateTime>(nullable: false),
                    MessageType = table.Column<bool>(nullable: false),
                    SenderID = table.Column<int>(nullable: false),
                    ReceiveID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_MESSAGES_USERS_ReceiveID",
                        column: x => x.ReceiveID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MESSAGES_USERS_SenderID",
                        column: x => x.SenderID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PARENTS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false),
                    Adress = table.Column<string>(maxLength: 500, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARENTS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_PARENTS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    TcNo = table.Column<string>(maxLength: 11, nullable: false),
                    StdNumber = table.Column<string>(maxLength: 20, nullable: false),
                    StdName = table.Column<string>(maxLength: 20, nullable: false),
                    StdSurname = table.Column<string>(maxLength: 20, nullable: true),
                    Adress1 = table.Column<string>(maxLength: 500, nullable: false),
                    Adress2 = table.Column<string>(maxLength: 500, nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    GraduateDate = table.Column<DateTime>(nullable: false),
                    BirthPlace = table.Column<string>(maxLength: 50, nullable: false),
                    BloodType = table.Column<string>(maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_STUDENTS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEACHERS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    TcNo = table.Column<string>(maxLength: 11, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false),
                    Adress = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    BranchID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEACHERS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_TEACHERS_BRANCHS_BranchID",
                        column: x => x.BranchID,
                        principalTable: "BRANCHS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEACHERS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCHOOLS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    EducationID = table.Column<int>(nullable: false),
                    SchoolName = table.Column<string>(maxLength: 50, nullable: false),
                    Adress = table.Column<string>(maxLength: 300, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHOOLS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SCHOOLS_EDUCATIONAL_INSTITUTIONs_EducationID",
                        column: x => x.EducationID,
                        principalTable: "EDUCATIONAL_INSTITUTIONs",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_PARENTS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    ParentID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_PARENTS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_STUDENT_PARENTS_PARENTS_ParentID",
                        column: x => x.ParentID,
                        principalTable: "PARENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_PARENTS_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLASSES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Class_Name = table.Column<string>(maxLength: 50, nullable: false),
                    SchoolID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_CLASSES_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NEWSES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    NewsDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    NewsContent = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 350, nullable: true),
                    EducationID = table.Column<int>(nullable: false),
                    SchoolID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NEWSES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_NEWSES_EDUCATIONAL_INSTITUTIONs_EducationID",
                        column: x => x.EducationID,
                        principalTable: "EDUCATIONAL_INSTITUTIONs",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NEWSES_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NEWSES_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCHOOL_EMPLOYERS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: true),
                    NameSurname = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    EducationID = table.Column<int>(nullable: false),
                    EmployerTypeID = table.Column<int>(nullable: false),
                    SchoolID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHOOL_EMPLOYERS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_EDUCATIONAL_INSTITUTIONs_EducationID",
                        column: x => x.EducationID,
                        principalTable: "EDUCATIONAL_INSTITUTIONs",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_EMPLOYEE_TYPES_EmployerTypeID",
                        column: x => x.EmployerTypeID,
                        principalTable: "EMPLOYEE_TYPES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SECTIONS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    SectionName = table.Column<string>(maxLength: 50, nullable: false),
                    SchoolID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECTIONS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SECTIONS_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLASS_SECTIONS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    ClassSectionName = table.Column<string>(maxLength: 50, nullable: false),
                    ClassID = table.Column<int>(nullable: false),
                    EducationTermID = table.Column<int>(nullable: false),
                    SectionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASS_SECTIONS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_CLASS_SECTIONS_CLASSES_ClassID",
                        column: x => x.ClassID,
                        principalTable: "CLASSES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLASS_SECTIONS_EDUCATIONAL_TERMS_EducationTermID",
                        column: x => x.EducationTermID,
                        principalTable: "EDUCATIONAL_TERMS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLASS_SECTIONS_SECTIONS_SectionID",
                        column: x => x.SectionID,
                        principalTable: "SECTIONS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_CLASSes",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    ClassSectionID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_CLASSes", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_STUDENT_CLASSes_CLASS_SECTIONS_ClassSectionID",
                        column: x => x.ClassSectionID,
                        principalTable: "CLASS_SECTIONS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_CLASSes_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLASS_SECTIONS_ClassID",
                table: "CLASS_SECTIONS",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_CLASS_SECTIONS_EducationTermID",
                table: "CLASS_SECTIONS",
                column: "EducationTermID");

            migrationBuilder.CreateIndex(
                name: "IX_CLASS_SECTIONS_SectionID",
                table: "CLASS_SECTIONS",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSES_SchoolID",
                table: "CLASSES",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_EDUCATIONAL_INSTITUTIONs_CityID",
                table: "EDUCATIONAL_INSTITUTIONs",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_FEEDBACKs_UserID",
                table: "FEEDBACKs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_ReceiveID",
                table: "MESSAGES",
                column: "ReceiveID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_SenderID",
                table: "MESSAGES",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_NEWSES_EducationID",
                table: "NEWSES",
                column: "EducationID");

            migrationBuilder.CreateIndex(
                name: "IX_NEWSES_SchoolID",
                table: "NEWSES",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_NEWSES_UserID",
                table: "NEWSES",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PARENTS_UserID",
                table: "PARENTS",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_EMPLOYERS_EducationID",
                table: "SCHOOL_EMPLOYERS",
                column: "EducationID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_EMPLOYERS_EmployerTypeID",
                table: "SCHOOL_EMPLOYERS",
                column: "EmployerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_EMPLOYERS_SchoolID",
                table: "SCHOOL_EMPLOYERS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_EMPLOYERS_UserID",
                table: "SCHOOL_EMPLOYERS",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOLS_EducationID",
                table: "SCHOOLS",
                column: "EducationID");

            migrationBuilder.CreateIndex(
                name: "IX_SECTIONS_SchoolID",
                table: "SECTIONS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_CLASSes_ClassSectionID",
                table: "STUDENT_CLASSes",
                column: "ClassSectionID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_CLASSes_StudentID",
                table: "STUDENT_CLASSes",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_PARENTS_ParentID",
                table: "STUDENT_PARENTS",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_PARENTS_StudentID",
                table: "STUDENT_PARENTS",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_UserID",
                table: "STUDENTS",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHERS_BranchID",
                table: "TEACHERS",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHERS_UserID",
                table: "TEACHERS",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FEEDBACKs");

            migrationBuilder.DropTable(
                name: "MESSAGES");

            migrationBuilder.DropTable(
                name: "NEWSES");

            migrationBuilder.DropTable(
                name: "SCHOOL_EMPLOYERS");

            migrationBuilder.DropTable(
                name: "STUDENT_CLASSes");

            migrationBuilder.DropTable(
                name: "STUDENT_PARENTS");

            migrationBuilder.DropTable(
                name: "TEACHERS");

            migrationBuilder.DropTable(
                name: "EMPLOYEE_TYPES");

            migrationBuilder.DropTable(
                name: "CLASS_SECTIONS");

            migrationBuilder.DropTable(
                name: "PARENTS");

            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "BRANCHS");

            migrationBuilder.DropTable(
                name: "CLASSES");

            migrationBuilder.DropTable(
                name: "EDUCATIONAL_TERMS");

            migrationBuilder.DropTable(
                name: "SECTIONS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "SCHOOLS");

            migrationBuilder.DropTable(
                name: "EDUCATIONAL_INSTITUTIONs");

            migrationBuilder.DropTable(
                name: "CITYS");
        }
    }
}
