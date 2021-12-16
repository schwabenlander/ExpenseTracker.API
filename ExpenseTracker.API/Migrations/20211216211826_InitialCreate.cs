using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ExpenseItems",
                columns: new[] { "Id", "Amount", "Date", "Title" },
                values: new object[,]
                {
                    { 1, 24.14m, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toilet Paper" },
                    { 2, 799.49m, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "New TV" },
                    { 3, 94.67m, new DateTime(2021, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cable Internet" },
                    { 4, 450.00m, new DateTime(2021, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Desk (Wooden)" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItems");
        }
    }
}
