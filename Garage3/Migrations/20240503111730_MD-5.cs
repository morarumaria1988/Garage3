using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class MD5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_TypeName",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType");

            migrationBuilder.RenameTable(
                name: "VehicleType",
                newName: "VTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VTypes",
                table: "VTypes",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VTypes_TypeName",
                table: "Vehicles",
                column: "TypeName",
                principalTable: "VTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VTypes_TypeName",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VTypes",
                table: "VTypes");

            migrationBuilder.RenameTable(
                name: "VTypes",
                newName: "VehicleType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleType_TypeName",
                table: "Vehicles",
                column: "TypeName",
                principalTable: "VehicleType",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
