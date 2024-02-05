using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3b153ed5-bbae-4cf4-bee8-07c30e66750f"), "Trattoria Roz Cafe" },
                    { new Guid("8e0be48e-7e12-4fcb-a8b1-12c2385716f4"), "Taverna Racilor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("3b153ed5-bbae-4cf4-bee8-07c30e66750f"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("8e0be48e-7e12-4fcb-a8b1-12c2385716f4"));

            
        }
    }
}
