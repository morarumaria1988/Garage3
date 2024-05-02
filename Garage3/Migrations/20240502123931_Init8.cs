using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Vehicles_FordonRegistrationNumber",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_FordonRegistrationNumber",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "FordonRegistrationNumber",
                table: "Receipts");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Receipts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_RegistrationNumber",
                table: "Receipts",
                column: "RegistrationNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Vehicles_RegistrationNumber",
                table: "Receipts",
                column: "RegistrationNumber",
                principalTable: "Vehicles",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Vehicles_RegistrationNumber",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_RegistrationNumber",
                table: "Receipts");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Receipts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FordonRegistrationNumber",
                table: "Receipts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_FordonRegistrationNumber",
                table: "Receipts",
                column: "FordonRegistrationNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Vehicles_FordonRegistrationNumber",
                table: "Receipts",
                column: "FordonRegistrationNumber",
                principalTable: "Vehicles",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
