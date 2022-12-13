using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.DataAccess.Migrations
{
    public partial class extendEmployeeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnnualLeaves",
                table: "EmployeeTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CasualLeaves",
                table: "EmployeeTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalLeaves",
                table: "EmployeeTypes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualLeaves",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "CasualLeaves",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "MedicalLeaves",
                table: "EmployeeTypes");
        }
    }
}
