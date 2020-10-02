using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableManager.WPF.Migrations
{
    public partial class sprint2release : Migration
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
                    DayName = table.Column<string>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    startHour = table.Column<string>(nullable: true),
                    startMin = table.Column<string>(nullable: true),
                    endHour = table.Column<string>(nullable: true),
                    endMin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                    table.UniqueConstraint("AK_Days_DayName", x => x.DayName);
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
                name: "GroupTTs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    groupId = table.Column<string>(nullable: true),
                    timeSlot = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    lecturer = table.Column<string>(nullable: true),
                    room = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTTs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LecturerTTs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    lecturer = table.Column<string>(nullable: true),
                    TimeSlot = table.Column<string>(nullable: true),
                    subjectName = table.Column<string>(nullable: true),
                    groupId = table.Column<string>(nullable: true),
                    room = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerTTs", x => x.Id);
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
                name: "RoomTTs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    roomName = table.Column<string>(nullable: true),
                    timeSlot = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    groupId = table.Column<string>(nullable: true),
                    lecturer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTTs", x => x.Id);
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
                name: "TimeSlots",
                columns: table => new
                {
                    CodeId = table.Column<string>(nullable: false),
                    DayName = table.Column<string>(nullable: true),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true),
                    sessionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.CodeId);
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
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectId = table.Column<int>(nullable: true),
                    TagId = table.Column<int>(nullable: true),
                    StudentCount = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    ConsecutiveSessionSessionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Sessions_ConsecutiveSessionSessionId",
                        column: x => x.ConsecutiveSessionSessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupIdUnavailableTimeSlot",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    TimeSlotId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupIdUnavailableTimeSlot", x => new { x.GroupId, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_GroupIdUnavailableTimeSlot_GroupIds_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupIdUnavailableTimeSlot_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "CodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupIdUnavailableTimeSlot",
                columns: table => new
                {
                    SubGroupId = table.Column<int>(nullable: false),
                    TimeSlotId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupIdUnavailableTimeSlot", x => new { x.SubGroupId, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_SubGroupIdUnavailableTimeSlot_SubGroupIds_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubGroupIdUnavailableTimeSlot_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "CodeId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "GroupIdSession",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupIdSession", x => new { x.GroupId, x.SessionId });
                    table.ForeignKey(
                        name: "FK_GroupIdSession_GroupIds_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupIdSession_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionUnavailableTimeSlot",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false),
                    TimeSlotId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionUnavailableTimeSlot", x => new { x.SessionId, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_SessionUnavailableTimeSlot_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionUnavailableTimeSlot_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "CodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupIdSession",
                columns: table => new
                {
                    SubGroupId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupIdSession", x => new { x.SubGroupId, x.SessionId });
                    table.ForeignKey(
                        name: "FK_SubGroupIdSession_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubGroupIdSession_SubGroupIds_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupIdPreferredRoom",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupIdPreferredRoom", x => new { x.GroupId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_GroupIdPreferredRoom_GroupIds_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupIdPreferredRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPreferredRoom",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPreferredRoom", x => new { x.SessionId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_SessionPreferredRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionPreferredRoom_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubGroupIdPrefferedRoom",
                columns: table => new
                {
                    SubGroupId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroupIdPrefferedRoom", x => new { x.SubGroupId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_SubGroupIdPrefferedRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubGroupIdPrefferedRoom_SubGroupIds_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroupIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPreferredRoom",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPreferredRoom", x => new { x.SubjectId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_SubjectPreferredRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectPreferredRoom_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagPreferredRoom",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPreferredRoom", x => new { x.TagId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_TagPreferredRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagPreferredRoom_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerPreferredRoom",
                columns: table => new
                {
                    LectuererId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerPreferredRoom", x => new { x.LectuererId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_LecturerPreferredRoom_Lecturers_LectuererId",
                        column: x => x.LectuererId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerPreferredRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerSession",
                columns: table => new
                {
                    LecturerId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerSession", x => new { x.LecturerId, x.SessionId });
                    table.ForeignKey(
                        name: "FK_LecturerSession_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerSession_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerUnavailableTimeSlot",
                columns: table => new
                {
                    LecturerId = table.Column<int>(nullable: false),
                    TimeSlotId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerUnavailableTimeSlot", x => new { x.LecturerId, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_LecturerUnavailableTimeSlot_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerUnavailableTimeSlot_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "CodeId",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 6, "Saturday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 5, "Friday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 4, "Thursday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 7, "Sunday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 2, "Tuesday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 1, "Monday", false, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "IsSelected", "endHour", "endMin", "startHour", "startMin" },
                values: new object[] { 3, "Wednesday", false, null, null, null, null });

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
                name: "IX_GroupIdPreferredRoom_RoomId",
                table: "GroupIdPreferredRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupIdSession_SessionId",
                table: "GroupIdSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupIdUnavailableTimeSlot_TimeSlotId",
                table: "GroupIdUnavailableTimeSlot",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerPreferredRoom_RoomId",
                table: "LecturerPreferredRoom",
                column: "RoomId");

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
                name: "IX_LecturerSession_SessionId",
                table: "LecturerSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerUnavailableTimeSlot_TimeSlotId",
                table: "LecturerUnavailableTimeSlot",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CenterId",
                table: "Rooms",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPreferredRoom_RoomId",
                table: "SessionPreferredRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ConsecutiveSessionSessionId",
                table: "Sessions",
                column: "ConsecutiveSessionSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SubjectId",
                table: "Sessions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TagId",
                table: "Sessions",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionUnavailableTimeSlot_TimeSlotId",
                table: "SessionUnavailableTimeSlot",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroupIdPrefferedRoom_RoomId",
                table: "SubGroupIdPrefferedRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroupIdSession_SessionId",
                table: "SubGroupIdSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroupIdUnavailableTimeSlot_TimeSlotId",
                table: "SubGroupIdUnavailableTimeSlot",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPreferredRoom_RoomId",
                table: "SubjectPreferredRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TagPreferredRoom_RoomId",
                table: "TagPreferredRoom",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "DaysAndHours");

            migrationBuilder.DropTable(
                name: "GroupIdPreferredRoom");

            migrationBuilder.DropTable(
                name: "GroupIdSession");

            migrationBuilder.DropTable(
                name: "GroupIdUnavailableTimeSlot");

            migrationBuilder.DropTable(
                name: "GroupNumbers");

            migrationBuilder.DropTable(
                name: "GroupTTs");

            migrationBuilder.DropTable(
                name: "LecturerPreferredRoom");

            migrationBuilder.DropTable(
                name: "LecturerSession");

            migrationBuilder.DropTable(
                name: "LecturerTTs");

            migrationBuilder.DropTable(
                name: "LecturerUnavailableTimeSlot");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "RoomTTs");

            migrationBuilder.DropTable(
                name: "SessionPreferredRoom");

            migrationBuilder.DropTable(
                name: "SessionUnavailableTimeSlot");

            migrationBuilder.DropTable(
                name: "SubGroupIdPrefferedRoom");

            migrationBuilder.DropTable(
                name: "SubGroupIdSession");

            migrationBuilder.DropTable(
                name: "SubGroupIdUnavailableTimeSlot");

            migrationBuilder.DropTable(
                name: "SubGroupNumbers");

            migrationBuilder.DropTable(
                name: "SubjectPreferredRoom");

            migrationBuilder.DropTable(
                name: "TagPreferredRoom");

            migrationBuilder.DropTable(
                name: "Year_Semesters");

            migrationBuilder.DropTable(
                name: "GroupIds");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "SubGroupIds");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Centers");
        }
    }
}
