using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.DataAccess.Migrations
{
    public partial class resetEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_AspNetUsers_UserCode",
                table: "EmployeeLeaves");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaves_UserCode",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "EmployeeLeaves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "EmployeeLeaves",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_UserCode",
                table: "EmployeeLeaves",
                column: "UserCode");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_AspNetUsers_UserCode",
                table: "EmployeeLeaves",
                column: "UserCode",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
