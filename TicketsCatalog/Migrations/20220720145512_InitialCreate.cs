using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsCatalog.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK001", 1.3, new DateTime(2022, 7, 20, 14, 55, 12, 235, DateTimeKind.Utc).AddTicks(7731) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK002", 1.5, new DateTime(2022, 7, 20, 14, 55, 12, 235, DateTimeKind.Utc).AddTicks(7734) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK003", 2.2999999999999998, new DateTime(2022, 7, 20, 14, 55, 12, 235, DateTimeKind.Utc).AddTicks(7736) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK004", 5.5, new DateTime(2022, 7, 20, 14, 55, 12, 235, DateTimeKind.Utc).AddTicks(7736) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
