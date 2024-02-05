using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("3b153ed5-bbae-4cf4-bee8-07c30e66750f"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("8e0be48e-7e12-4fcb-a8b1-12c2385716f4"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ceed4e6b-7808-4acb-bfbc-1151496b433a"), "Trattoria Roz Cafe" },
                    { new Guid("ec712ef7-842c-4251-87a9-d675c0200d19"), "Taverna Racilor" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("00225c19-fb6c-4f1b-8cfb-2756892c8292"), new Guid("ceed4e6b-7808-4acb-bfbc-1151496b433a") },
                    { new Guid("83e8f234-1c7e-4c2d-b535-92b0330e3bf5"), new Guid("ec712ef7-842c-4251-87a9-d675c0200d19") }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "MenuId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0d68d0b4-0e90-4a50-aa7d-62fcf4558a23"), "Description for Dish 1", new Guid("00225c19-fb6c-4f1b-8cfb-2756892c8292"), "Dish 1", 10m, 100 },
                    { new Guid("9ad81eda-4d08-490b-ae2e-29029cba85ff"), "Description for Dish 2", new Guid("83e8f234-1c7e-4c2d-b535-92b0330e3bf5"), "Dish 2", 15m, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("0d68d0b4-0e90-4a50-aa7d-62fcf4558a23"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("9ad81eda-4d08-490b-ae2e-29029cba85ff"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("00225c19-fb6c-4f1b-8cfb-2756892c8292"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("83e8f234-1c7e-4c2d-b535-92b0330e3bf5"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("ceed4e6b-7808-4acb-bfbc-1151496b433a"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("ec712ef7-842c-4251-87a9-d675c0200d19"));

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3b153ed5-bbae-4cf4-bee8-07c30e66750f"), "Trattoria Roz Cafe" },
                    { new Guid("8e0be48e-7e12-4fcb-a8b1-12c2385716f4"), "Taverna Racilor" }
                });
        }
    }
}
