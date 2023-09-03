using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvp_studio_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedDurationWeekToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration_Week",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration_Week",
                table: "Project");
        }
    }
}
