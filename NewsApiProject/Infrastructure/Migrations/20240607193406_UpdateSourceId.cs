using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSourceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceKind",
                table: "Sources");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sources",
                newName: "Unique");

            migrationBuilder.AddColumn<string>(
                name: "SourceId",
                table: "Sources",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Sources");

            migrationBuilder.RenameColumn(
                name: "Unique",
                table: "Sources",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "SourceKind",
                table: "Sources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
