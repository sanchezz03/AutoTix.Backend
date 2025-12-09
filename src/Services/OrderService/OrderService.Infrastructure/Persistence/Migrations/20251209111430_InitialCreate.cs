using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReservations_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StationTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TicketReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripSnapshots_TicketReservations_TicketReservationId",
                        column: x => x.TicketReservationId,
                        principalTable: "TicketReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripSegmentSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    DepartAt = table.Column<long>(type: "bigint", nullable: false),
                    ArriveAt = table.Column<long>(type: "bigint", nullable: false),
                    StationFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StationTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StationsTimeOffset = table.Column<int>(type: "int", nullable: false),
                    TripSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSegmentSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripSegmentSnapshots_TripSnapshots_TripSnapshotId",
                        column: x => x.TripSnapshotId,
                        principalTable: "TripSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    StationFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StationTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WagonClassesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripSegmentSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainSnapshots_TripSegmentSnapshots_TripSegmentSnapshotId",
                        column: x => x.TripSegmentSnapshotId,
                        principalTable: "TripSegmentSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketReservations_OrderId",
                table: "TicketReservations",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainSnapshots_TripSegmentSnapshotId",
                table: "TrainSnapshots",
                column: "TripSegmentSnapshotId",
                unique: true,
                filter: "[TripSegmentSnapshotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentSnapshots_TripSnapshotId",
                table: "TripSegmentSnapshots",
                column: "TripSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSnapshots_TicketReservationId",
                table: "TripSnapshots",
                column: "TicketReservationId",
                unique: true,
                filter: "[TicketReservationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainSnapshots");

            migrationBuilder.DropTable(
                name: "TripSegmentSnapshots");

            migrationBuilder.DropTable(
                name: "TripSnapshots");

            migrationBuilder.DropTable(
                name: "TicketReservations");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
