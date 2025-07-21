using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SirketData.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectProducts");
        }
    }
}
