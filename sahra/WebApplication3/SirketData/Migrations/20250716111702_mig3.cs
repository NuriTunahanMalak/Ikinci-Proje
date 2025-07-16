using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SirketData.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Sensors",
                newName: "SensorType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SensorType",
                table: "Sensors",
                newName: "Type");
        }
    }
}
