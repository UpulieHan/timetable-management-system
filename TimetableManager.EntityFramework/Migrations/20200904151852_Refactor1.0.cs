using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableManager.EntityFramework.Migrations
{
    public partial class Refactor10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    CenterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CenterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.CenterId);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayName = table.Column<string>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "DaysAndHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NoOfDays = table.Column<int>(nullable: false),
                    Hours = table.Column<int>(nullable: false),
                    Mins = table.Column<int>(nullable: false),
                    TimeSlot = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysAndHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FacultyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "GroupIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupIds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupNum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LevelId = table.Column<int>(nullable: false),
                    LevelName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.UniqueConstraint("AK_Levels_LevelId", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    ProgrammeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgrammeFullName = table.Column<string>(nullable: true),
                    ProgrammeShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.ProgrammeId);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubGroupID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupIds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubGroupNum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectName = table.Column<string>(nullable: true),
                    SubjectCode = table.Column<string>(nullable: true),
                    LectureHours = table.Column<int>(nullable: false),
                    TutorialHours = table.Column<int>(nullable: false),
                    LabHours = table.Column<int>(nullable: false),
                    EvaluationHours = table.Column<int>(nullable: false),
                    OfferedYearSemester = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Year_Semesters",
                columns: table => new
                {
                    YsId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YsYear = table.Column<string>(nullable: true),
                    YsSemester = table.Column<string>(nullable: true),
                    YsShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year_Semesters", x => x.YsId);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuildingName = table.Column<string>(nullable: true),
                    CenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Buildings_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentName = table.Column<string>(nullable: true),
                    FacultyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomName = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    BuildingId = table.Column<int>(nullable: true),
                    CenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    FacultyId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    CenterId = table.Column<int>(nullable: true),
                    BuildingId = table.Column<int>(nullable: true),
                    LevelId = table.Column<int>(nullable: true),
                    Rank = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.UniqueConstraint("AK_Lecturers_EmployeeId", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Lecturers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturers_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturers_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Centers",
                columns: new[] { "CenterId", "CenterName" },
                values: new object[] { 1, "Malabe" });

            migrationBuilder.InsertData(
                table: "Centers",
                columns: new[] { "CenterId", "CenterName" },
                values: new object[] { 2, "Matara" });

            migrationBuilder.InsertData(
                table: "Centers",
                columns: new[] { "CenterId", "CenterName" },
                values: new object[] { 3, "Kandy" });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 6, "Saturday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 5, "Friday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 4, "Thursday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 7, "Sunday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 2, "Tuesday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 1, "Monday", false });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected" },
                values: new object[] { 3, "Wednesday", false });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "FacultyName" },
                values: new object[] { 1, "Computing" });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "FacultyName" },
                values: new object[] { 2, "Engineering" });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "FacultyName" },
                values: new object[] { 3, "Business" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 6, 6, "Assistant Lecturer" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 1, 1, "Professor" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 2, 2, "Assistant Professor" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 3, 3, "Senior Lecturer(HG)" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 4, 4, "Senior Lecturer" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 5, 5, "Lecturer" });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "LevelId", "LevelName" },
                values: new object[] { 7, 7, "Instructors" });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CenterId",
                table: "Buildings",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_BuildingId",
                table: "Lecturers",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_CenterId",
                table: "Lecturers",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_FacultyId",
                table: "Lecturers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LevelId",
                table: "Lecturers",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CenterId",
                table: "Rooms",
                column: "CenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "DaysAndHours");

            migrationBuilder.DropTable(
                name: "GroupIds");

            migrationBuilder.DropTable(
                name: "GroupNumbers");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SubGroupIds");

            migrationBuilder.DropTable(
                name: "SubGroupNumbers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Year_Semesters");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Centers");
        }
    }
}
