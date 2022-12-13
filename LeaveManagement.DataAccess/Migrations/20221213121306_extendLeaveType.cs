using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.DataAccess.Migrations
{
    public partial class extendLeaveType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalLeaves",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLeaves",
                table: "LeaveTypes");
        }
    }
}
