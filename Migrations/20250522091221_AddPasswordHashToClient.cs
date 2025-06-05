using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "People",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
