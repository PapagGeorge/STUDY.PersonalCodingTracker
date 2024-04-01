using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class TransactionConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Accommodation_AccommodationId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Packages_PackageId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Service_ServiceId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Transportation_TransportationId",
                table: "Transaction");

            migrationBuilder.AlterColumn<long>(
                name: "TransportationId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 1, 17, 19, 30, 783, DateTimeKind.Local).AddTicks(5320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PackageId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AccommodationId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Accommodation_AccommodationId",
                table: "Transaction",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Packages_PackageId",
                table: "Transaction",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Service_ServiceId",
                table: "Transaction",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Transportation_TransportationId",
                table: "Transaction",
                column: "TransportationId",
                principalTable: "Transportation",
                principalColumn: "TransportationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Accommodation_AccommodationId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Packages_PackageId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Service_ServiceId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Transportation_TransportationId",
                table: "Transaction");

            migrationBuilder.AlterColumn<long>(
                name: "TransportationId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 1, 17, 19, 30, 783, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PackageId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccommodationId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Accommodation_AccommodationId",
                table: "Transaction",
                column: "AccommodationId",
                principalTable: "Accommodation",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Packages_PackageId",
                table: "Transaction",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Service_ServiceId",
                table: "Transaction",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Transportation_TransportationId",
                table: "Transaction",
                column: "TransportationId",
                principalTable: "Transportation",
                principalColumn: "TransportationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
