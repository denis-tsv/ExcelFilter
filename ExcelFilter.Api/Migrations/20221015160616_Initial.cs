using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelFilter.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "City 2" },
                    { 2, "City 1" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CityId", "CreatedAt", "Name", "Price" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2021, 9, 14, 23, 6, 16, 648, DateTimeKind.Local).AddTicks(1961), "Order 3", 50m },
                    { 4, null, new DateTime(2022, 9, 14, 23, 6, 16, 648, DateTimeKind.Local).AddTicks(1964), null, 10m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CityId", "CreatedAt", "Name", "Price" },
                values: new object[] { 1, 1, new DateTime(2022, 10, 15, 23, 6, 16, 648, DateTimeKind.Local).AddTicks(1942), null, 100.1m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CityId", "CreatedAt", "Name", "Price" },
                values: new object[] { 2, 2, new DateTime(2023, 11, 16, 23, 6, 16, 648, DateTimeKind.Local).AddTicks(1954), "Order 2", 20.25m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CityId", "CreatedAt", "Name", "Price" },
                values: new object[] { 5, 1, new DateTime(2022, 9, 14, 23, 6, 16, 648, DateTimeKind.Local).AddTicks(1965), "Order 1", 15m });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityId",
                table: "Orders",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
