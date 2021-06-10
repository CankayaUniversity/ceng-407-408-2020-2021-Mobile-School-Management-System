using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilOkulProc.WebAPI.Migrations
{
    public partial class LatestMg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "GRADETYPES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    GradeName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADETYPES", x => x.ObjectID);
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
                name: "zUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FEEDBACKS",
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
                    table.PrimaryKey("PK_FEEDBACKS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_FEEDBACKS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGES_USERS_SenderID",
                        column: x => x.SenderID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                    GoogleMap = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TEACHERS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    Revoked = table.Column<DateTime>(nullable: true),
                    RevokedByIp = table.Column<string>(nullable: true),
                    ReplacedByToken = table.Column<string>(nullable: true),
                    zUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_zUsers_zUserId",
                        column: x => x.zUserId,
                        principalTable: "zUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ABSENCES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    AbsenceDate = table.Column<DateTime>(nullable: false),
                    AbsenceDetails = table.Column<string>(maxLength: 500, nullable: false),
                    AbsenceCommentary = table.Column<string>(maxLength: 500, nullable: false),
                    TotalAbsence = table.Column<double>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABSENCES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_ABSENCES_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_STUDENT_PARENTS_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LECTURES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    LectureName = table.Column<string>(maxLength: 100, nullable: false),
                    StudentsID = table.Column<int>(nullable: false),
                    TeacherID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LECTURES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_LECTURES_STUDENTS_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LECTURES_TEACHERS_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "TEACHERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NEWSES_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NEWSES_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_EMPLOYEE_TYPES_EmployerTypeID",
                        column: x => x.EmployerTypeID,
                        principalTable: "EMPLOYEE_TYPES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_SCHOOLS_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "SCHOOLS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SCHOOL_EMPLOYERS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SCHOOL_STUDENTS_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TEACHER_SCHOOLS_TEACHERS_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "TEACHERS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EXAMS",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    ExamDate = table.Column<DateTime>(nullable: false),
                    ExamDetails = table.Column<string>(maxLength: 500, nullable: false),
                    LectureID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXAMS", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_EXAMS_LECTURES_LectureID",
                        column: x => x.LectureID,
                        principalTable: "LECTURES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GRADES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Grade = table.Column<double>(nullable: false),
                    LectureID = table.Column<int>(nullable: false),
                    GradeTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_GRADES_GRADETYPES_GradeTypeID",
                        column: x => x.GradeTypeID,
                        principalTable: "GRADETYPES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GRADES_LECTURES_LectureID",
                        column: x => x.LectureID,
                        principalTable: "LECTURES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYLLABUSES",
                columns: table => new
                {
                    ObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LectureID = table.Column<int>(nullable: false),
                    DaysID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYLLABUSES", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_SYLLABUSES_DAYSES_DaysID",
                        column: x => x.DaysID,
                        principalTable: "DAYSES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SYLLABUSES_LECTURES_LectureID",
                        column: x => x.LectureID,
                        principalTable: "LECTURES",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CLASS_SECTIONS_EDUCATIONAL_TERMS_EducationTermID",
                        column: x => x.EducationTermID,
                        principalTable: "EDUCATIONAL_TERMS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CLASS_SECTIONS_SECTIONS_SectionID",
                        column: x => x.SectionID,
                        principalTable: "SECTIONS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_STUDENT_CLASSes_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABSENCES_StudentID",
                table: "ABSENCES",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_EXAMS_LectureID",
                table: "EXAMS",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_FEEDBACKS_UserID",
                table: "FEEDBACKS",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_GradeTypeID",
                table: "GRADES",
                column: "GradeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_LectureID",
                table: "GRADES",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_StudentsID",
                table: "LECTURES",
                column: "StudentsID");

            migrationBuilder.CreateIndex(
                name: "IX_LECTURES_TeacherID",
                table: "LECTURES",
                column: "TeacherID");

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
                name: "IX_RefreshToken_zUserId",
                table: "RefreshToken",
                column: "zUserId");

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
                name: "IX_SCHOOL_STUDENTS_SchoolID",
                table: "SCHOOL_STUDENTS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOOL_STUDENTS_StudentID",
                table: "SCHOOL_STUDENTS",
                column: "StudentID");

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
                name: "IX_SYLLABUSES_DaysID",
                table: "SYLLABUSES",
                column: "DaysID");

            migrationBuilder.CreateIndex(
                name: "IX_SYLLABUSES_LectureID",
                table: "SYLLABUSES",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOLS_SchoolID",
                table: "TEACHER_SCHOOLS",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHER_SCHOOLS_TeacherID",
                table: "TEACHER_SCHOOLS",
                column: "TeacherID");

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
                name: "ABSENCES");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EXAMS");

            migrationBuilder.DropTable(
                name: "FEEDBACKS");

            migrationBuilder.DropTable(
                name: "GRADES");

            migrationBuilder.DropTable(
                name: "MESSAGES");

            migrationBuilder.DropTable(
                name: "NEWSES");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SCHOOL_EMPLOYERS");

            migrationBuilder.DropTable(
                name: "SCHOOL_STUDENTS");

            migrationBuilder.DropTable(
                name: "STUDENT_CLASSes");

            migrationBuilder.DropTable(
                name: "STUDENT_PARENTS");

            migrationBuilder.DropTable(
                name: "SYLLABUSES");

            migrationBuilder.DropTable(
                name: "TEACHER_SCHOOLS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GRADETYPES");

            migrationBuilder.DropTable(
                name: "zUsers");

            migrationBuilder.DropTable(
                name: "EMPLOYEE_TYPES");

            migrationBuilder.DropTable(
                name: "CLASS_SECTIONS");

            migrationBuilder.DropTable(
                name: "PARENTS");

            migrationBuilder.DropTable(
                name: "DAYSES");

            migrationBuilder.DropTable(
                name: "LECTURES");

            migrationBuilder.DropTable(
                name: "CLASSES");

            migrationBuilder.DropTable(
                name: "EDUCATIONAL_TERMS");

            migrationBuilder.DropTable(
                name: "SECTIONS");

            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "TEACHERS");

            migrationBuilder.DropTable(
                name: "SCHOOLS");

            migrationBuilder.DropTable(
                name: "BRANCHS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "EDUCATIONAL_INSTITUTIONs");

            migrationBuilder.DropTable(
                name: "CITYS");
        }
    }
}
