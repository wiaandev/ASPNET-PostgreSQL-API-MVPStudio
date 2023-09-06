using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvp_studio_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTeamAndTeamMembersModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_Employee_EmployeeId",
                table: "TeamMember");

            migrationBuilder.DropIndex(
                name: "IX_TeamMember_EmployeeId",
                table: "TeamMember");

            migrationBuilder.AlterColumn<List<int>>(
                name: "EmployeeId",
                table: "TeamMember",
                type: "integer[]",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Team",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TeamMember",
                type: "integer",
                nullable: false,
                oldClrType: typeof(List<int>),
                oldType: "integer[]");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Team",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_EmployeeId",
                table: "TeamMember",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_Employee_EmployeeId",
                table: "TeamMember",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
