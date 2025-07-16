using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YeniProjeDeneme1.Migrations
{
    /// <inheritdoc />
    public partial class SensorIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_Data_Sensor_sensor_id",
                table: "Sensor_Data");

            migrationBuilder.DropIndex(
                name: "IX_Sensor_Data_sensor_id",
                table: "Sensor_Data");

            migrationBuilder.DropColumn(
                name: "sensor_id",
                table: "Sensor_Data");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_Data_SensorId",
                table: "Sensor_Data",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_Data_Sensor_SensorId",
                table: "Sensor_Data",
                column: "SensorId",
                principalTable: "Sensor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_Data_Sensor_SensorId",
                table: "Sensor_Data");

            migrationBuilder.DropIndex(
                name: "IX_Sensor_Data_SensorId",
                table: "Sensor_Data");

            migrationBuilder.AddColumn<int>(
                name: "sensor_id",
                table: "Sensor_Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_Data_sensor_id",
                table: "Sensor_Data",
                column: "sensor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_Data_Sensor_sensor_id",
                table: "Sensor_Data",
                column: "sensor_id",
                principalTable: "Sensor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
