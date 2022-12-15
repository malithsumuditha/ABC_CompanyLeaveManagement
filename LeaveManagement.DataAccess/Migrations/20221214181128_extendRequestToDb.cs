using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.DataAccess.Migrations
{
    public partial class extendRequestToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequestLeaves_LeaveTypeId",
                table: "RequestLeaves",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLeaves_LeaveTypes_LeaveTypeId",
                table: "RequestLeaves",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "LeaveTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLeaves_LeaveTypes_LeaveTypeId",
                table: "RequestLeaves");

            migrationBuilder.DropIndex(
                name: "IX_RequestLeaves_LeaveTypeId",
                table: "RequestLeaves");
        }
    }
}
