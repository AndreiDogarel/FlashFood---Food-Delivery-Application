using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestaurantEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }
    }
}
