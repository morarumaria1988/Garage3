using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_MemberPersonalNumber",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "MemberPersonalNumber",
                table: "Vehicles",
                newName: "Personalnumber");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_MemberPersonalNumber",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_Personalnumber",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Personalnumber",
                table: "Vehicles",
                newName: "MemberPersonalNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_Personalnumber",
                table: "Vehicles",
                newName: "IX_Vehicles_MemberPersonalNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_MemberPersonalNumber",
                table: "Vehicles",
                column: "MemberPersonalNumber",
                principalTable: "Customers",
                principalColumn: "PersonalNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
