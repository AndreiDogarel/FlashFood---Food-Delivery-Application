using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5ab18dab-5602-44a4-8a95-93ae40ed800d"), "Calif Kebab" },
                    { new Guid("8c6179b2-bb7d-4522-9142-f3e0e506df15"), "Jerry's Pizza" },
                    { new Guid("9c767028-07b5-44bf-82b8-1a15ee2f2b2f"), "Olga's Caffe" },
                    { new Guid("c3ceb729-077e-46e8-bafd-27e603f7fc04"), "Shaormeria Baneasa" },
                    { new Guid("c6d64e00-48bf-459c-88b4-4cc2c5892ed3"), "Trattoria Roz Cafe" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("0b53c95e-a07f-482b-9c44-3388582ea72d"), new Guid("c3ceb729-077e-46e8-bafd-27e603f7fc04") },
                    { new Guid("1764c805-7f75-4d3e-b544-5bdefc1c7dc3"), new Guid("9c767028-07b5-44bf-82b8-1a15ee2f2b2f") },
                    { new Guid("2a80258b-a3c0-4886-bd7f-6393a927ca04"), new Guid("5ab18dab-5602-44a4-8a95-93ae40ed800d") },
                    { new Guid("5309312a-14d8-4194-960c-24a15094b246"), new Guid("c6d64e00-48bf-459c-88b4-4cc2c5892ed3") },
                    { new Guid("fceb7777-ac3c-4c13-a57a-09da1c2a5f6b"), new Guid("8c6179b2-bb7d-4522-9142-f3e0e506df15") }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "MenuId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("03d0a037-d67a-4a52-82a6-1c9d566d281f"), null, new Guid("1764c805-7f75-4d3e-b544-5bdefc1c7dc3"), "Pizza Pollo", 25m, 400 },
                    { new Guid("177826a4-094e-44a7-a74e-fc603945b5b2"), null, new Guid("fceb7777-ac3c-4c13-a57a-09da1c2a5f6b"), "Pizza Diavola", 25m, 400 },
                    { new Guid("3834598d-98da-4a1f-92cb-58684fc78192"), null, new Guid("fceb7777-ac3c-4c13-a57a-09da1c2a5f6b"), "Pizza Classica", 40m, 500 },
                    { new Guid("38d7f49f-0d55-4862-a65c-42fee0705d56"), null, new Guid("0b53c95e-a07f-482b-9c44-3388582ea72d"), "Shaorma de Pui", 30m, 650 },
                    { new Guid("5952f99f-992b-49a9-b471-2da54303817f"), null, new Guid("5309312a-14d8-4194-960c-24a15094b246"), "Burger de Vita", 50m, 650 },
                    { new Guid("b2c1879f-18a6-4578-b230-111e7c9fd10d"), null, new Guid("2a80258b-a3c0-4886-bd7f-6393a927ca04"), "Cheese Kebab de Pui", 50m, 500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("03d0a037-d67a-4a52-82a6-1c9d566d281f"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("177826a4-094e-44a7-a74e-fc603945b5b2"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("3834598d-98da-4a1f-92cb-58684fc78192"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("38d7f49f-0d55-4862-a65c-42fee0705d56"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("5952f99f-992b-49a9-b471-2da54303817f"));

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("b2c1879f-18a6-4578-b230-111e7c9fd10d"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("0b53c95e-a07f-482b-9c44-3388582ea72d"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("1764c805-7f75-4d3e-b544-5bdefc1c7dc3"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("2a80258b-a3c0-4886-bd7f-6393a927ca04"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("5309312a-14d8-4194-960c-24a15094b246"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("fceb7777-ac3c-4c13-a57a-09da1c2a5f6b"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("5ab18dab-5602-44a4-8a95-93ae40ed800d"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("8c6179b2-bb7d-4522-9142-f3e0e506df15"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("9c767028-07b5-44bf-82b8-1a15ee2f2b2f"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c3ceb729-077e-46e8-bafd-27e603f7fc04"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c6d64e00-48bf-459c-88b4-4cc2c5892ed3"));

            

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
    }
}
