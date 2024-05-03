using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class MD1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VTypeName",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VTypeName",
                table: "Vehicles",
                column: "VTypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeName",
                table: "Vehicles",
                column: "VTypeName",
                principalTable: "VehicleType",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleType_VTypeName",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VTypeName",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VTypeName",
                table: "Vehicles");
        }
    }
}
