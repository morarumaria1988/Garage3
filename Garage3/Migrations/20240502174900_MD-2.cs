using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class MD2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeName",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType");

            migrationBuilder.RenameColumn(
                name: "VTypeName",
                table: "Vehicles",
                newName: "VTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VTypeName",
                table: "Vehicles",
                newName: "IX_Vehicles_VTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "VehicleType",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeId",
                table: "Vehicles",
                column: "VTypeId",
                principalTable: "VehicleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VehicleType");

            migrationBuilder.RenameColumn(
                name: "VTypeId",
                table: "Vehicles",
                newName: "VTypeName");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VTypeId",
                table: "Vehicles",
                newName: "IX_Vehicles_VTypeName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleType",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                table: "VehicleType",
                column: "Name");

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
