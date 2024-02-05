using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "825b7c82-4b83-4edf-8bae-16cafe96f1fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f819f5e8-7acd-48b5-9505-0139826829a1");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3119ed38-7d0b-4ddb-8087-9d339a5eeead", "2", "User", "User" },
                    { "80abc758-e76d-4b2f-b7fb-e892342acb4e", "1", "Admin", "Admin" }
                });
        }
    }
}
