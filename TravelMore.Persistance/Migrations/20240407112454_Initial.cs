using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Balance_Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaxNumberOfGuests = table.Column<short>(type: "smallint", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    PricePerDay_Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Users_HostId",
                        column: x => x.HostId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayerId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    Fee_Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    Payment_Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    TotalPayment_Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentsDetails_Users_HostId",
                        column: x => x.HostId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentsDetails_Users_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    BookedHotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Details_NumberOfDays = table.Column<short>(type: "smallint", nullable: false),
                    Details_NumberOfGuests = table.Column<short>(type: "smallint", nullable: false),
                    Details_Schedule_From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details_Schedule_To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Hotels_BookedHotelId",
                        column: x => x.BookedHotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_PaymentsDetails_PaymentDetailsId",
                        column: x => x.PaymentDetailsId,
                        principalTable: "PaymentsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookedHotelId",
                table: "Bookings",
                column: "BookedHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuestId",
                table: "Bookings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentDetailsId",
                table: "Bookings",
                column: "PaymentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HostId",
                table: "Hotels",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsDetails_HostId",
                table: "PaymentsDetails",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsDetails_PayerId",
                table: "PaymentsDetails",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "PaymentsDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
