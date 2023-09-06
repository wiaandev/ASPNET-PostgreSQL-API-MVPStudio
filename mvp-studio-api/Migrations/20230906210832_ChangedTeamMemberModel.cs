using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvp_studio_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTeamMemberModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TeamMember",
                type: "integer",
                nullable: false,
                oldClrType: typeof(List<int>),
                oldType: "integer[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<int>>(
                name: "EmployeeId",
                table: "TeamMember",
                type: "integer[]",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
