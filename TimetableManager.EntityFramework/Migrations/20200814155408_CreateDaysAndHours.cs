using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableManager.EntityFramework.Migrations
{
    public partial class CreateDaysAndHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaysAndHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayName = table.Column<string>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false),
                    DaysAndHoursId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                    table.ForeignKey(
                        name: "FK_Days_DaysAndHours_DaysAndHoursId",
                        column: x => x.DaysAndHoursId,
                        principalTable: "DaysAndHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName", "DaysAndHoursId", "IsSelected" },
                values: new object[,]
                {
                    { 1, "Monday", null, false },
                    { 2, "Tuesday", null, false },
                    { 3, "Wednesday", null, false },
                    { 4, "Thursday", null, false },
                    { 5, "Friday", null, false },
                    { 6, "Saturday", null, false },
                    { 7, "Sunday", null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_DaysAndHoursId",
                table: "Days",
                column: "DaysAndHoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "DaysAndHours");
        }
    }
}
