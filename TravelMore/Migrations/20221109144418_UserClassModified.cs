using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMore.Migrations
{
    /// <inheritdoc />
    public partial class UserClassModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_UserId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Hotels");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnedHotelID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnedHotelID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Hotels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_UserId1",
                table: "Hotels",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId1",
                table: "Hotels",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
