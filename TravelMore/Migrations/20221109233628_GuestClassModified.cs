using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMore.Migrations
{
    /// <inheritdoc />
    public partial class GuestClassModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Guest",
                newName: "GuestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Guest",
                newName: "Id");
        }
    }
}
