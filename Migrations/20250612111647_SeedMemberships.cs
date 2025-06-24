using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedMemberships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Description", "DurationInMonths", "PlanName", "PricePerMonth" },
                values: new object[,]
                {
                    { 1, "Access during staffed hours", 1, "Basic", 29.99m },
                    { 2, "24/7 access + monthly trainer session", 1, "Premium", 49.99m },
                    { 3, "Unlimited access + trainer + nutrition", 1, "Elite", 79.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
