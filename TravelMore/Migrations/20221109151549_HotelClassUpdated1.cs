using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMore.Migrations
{
    /// <inheritdoc />
    public partial class HotelClassUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistanceToCenter",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceToCenter",
                table: "Hotels");
        }
    }
}
