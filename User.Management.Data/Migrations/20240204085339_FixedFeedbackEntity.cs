using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedFeedbackEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Feedbacks");

            
        }
    }
}
