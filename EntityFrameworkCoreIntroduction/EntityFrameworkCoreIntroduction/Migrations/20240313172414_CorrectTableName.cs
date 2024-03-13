using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCoreIntroduction.Migrations
{
    /// <inheritdoc />
    public partial class CorrectTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDEtails_Employees_EmployeeId",
                table: "EmployeeDEtails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDEtails",
                table: "EmployeeDEtails");

            migrationBuilder.RenameTable(
                name: "EmployeeDEtails",
                newName: "EmployeeDetails");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDEtails_EmployeeId",
                table: "EmployeeDetails",
                newName: "IX_EmployeeDetails_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDetails",
                table: "EmployeeDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDetails",
                table: "EmployeeDetails");

            migrationBuilder.RenameTable(
                name: "EmployeeDetails",
                newName: "EmployeeDEtails");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDEtails",
                newName: "IX_EmployeeDEtails_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDEtails",
                table: "EmployeeDEtails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDEtails_Employees_EmployeeId",
                table: "EmployeeDEtails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
