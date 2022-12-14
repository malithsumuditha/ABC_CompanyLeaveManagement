using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.DataAccess.Migrations
{
    public partial class AddEmployeeTypeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeTypeId",
                table: "AspNetUsers",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmployeeTypes_EmployeeTypeId",
                table: "AspNetUsers",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmployeeTypes_EmployeeTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "AspNetUsers");
        }
    }
}
