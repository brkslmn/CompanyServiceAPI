using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyServiceAPI.Migrations
{
    public partial class UpdateModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Employee_EmployeeId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_EmployeeId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_EmployeeId",
                table: "Department",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Employee_EmployeeId",
                table: "Department",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
