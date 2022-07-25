using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVM.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "TicketSales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketId = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSales", x => x.SaleId);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK001", 1.3, new DateTime(2022, 7, 22, 8, 6, 32, 358, DateTimeKind.Utc).AddTicks(649) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK002", 1.5, new DateTime(2022, 7, 22, 8, 6, 32, 358, DateTimeKind.Utc).AddTicks(656) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK003", 2.2999999999999998, new DateTime(2022, 7, 22, 8, 6, 32, 358, DateTimeKind.Utc).AddTicks(657) });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Price", "ValidFrom" },
                values: new object[] { "TK004", 5.5, new DateTime(2022, 7, 22, 8, 6, 32, 358, DateTimeKind.Utc).AddTicks(657) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketSales");
        }
    }
}
