using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableManager.EntityFramework.Migrations
{
    public partial class YearSemesterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Year_Semesters",
                columns: table => new
                {
                    YsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YsYear = table.Column<string>(nullable: true),
                    YsSemester = table.Column<string>(nullable: true),
                    YsShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year_Semesters", x => x.YsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Year_Semesters");
        }
    }
}
