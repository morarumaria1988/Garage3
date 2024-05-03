using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class MD4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeName",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "VTypeName",
                table: "Vehicles",
                newName: "TypeName");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VTypeName",
                table: "Vehicles",
                newName: "IX_Vehicles_TypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleType_TypeName",
                table: "Vehicles",
                column: "TypeName",
                principalTable: "VehicleType",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_TypeName",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "Vehicles",
                newName: "VTypeName");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_TypeName",
                table: "Vehicles",
                newName: "IX_Vehicles_VTypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeName",
                table: "Vehicles",
                column: "VTypeName",
                principalTable: "VehicleType",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
