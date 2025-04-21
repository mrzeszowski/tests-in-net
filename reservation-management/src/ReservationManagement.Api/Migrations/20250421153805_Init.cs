using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ReservationManagement");

            migrationBuilder.CreateTable(
                name: "Reservation",
                schema: "ReservationManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationResource",
                schema: "ReservationManagement",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationResource", x => new { x.ResourceId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_ReservationResource_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "ReservationManagement",
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Code",
                schema: "ReservationManagement",
                table: "Reservation",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationResource_ReservationId",
                schema: "ReservationManagement",
                table: "ReservationResource",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationResource_ResourceId",
                schema: "ReservationManagement",
                table: "ReservationResource",
                column: "ResourceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationResource",
                schema: "ReservationManagement");

            migrationBuilder.DropTable(
                name: "Reservation",
                schema: "ReservationManagement");
        }
    }
}
