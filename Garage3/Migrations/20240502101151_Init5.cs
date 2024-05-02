using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_Personalnumber",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Personalnumber",
                table: "Vehicles",
                newName: "PersonalNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_Personalnumber",
                table: "Vehicles",
                newName: "IX_Vehicles_PersonalNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_PersonalNumber",
                table: "Vehicles",
                column: "PersonalNumber",
                principalTable: "Customers",
                principalColumn: "PersonalNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_PersonalNumber",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "PersonalNumber",
                table: "Vehicles",
                newName: "Personalnumber");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_PersonalNumber",
                table: "Vehicles",
                newName: "IX_Vehicles_Personalnumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_Personalnumber",
                table: "Vehicles",
                column: "Personalnumber",
                principalTable: "Customers",
                principalColumn: "PersonalNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
