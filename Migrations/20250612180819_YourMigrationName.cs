using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "Id", "Capacity", "Name", "SpotsLeft", "StartTime", "TrainerId", "TrainerName" },
                values: new object[] { 6, 20, "Cardio Blast", 20, new DateTime(2025, 6, 18, 10, 0, 0, 0, DateTimeKind.Utc), 1, "John Doe" });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Expertise", "FullName" },
                values: new object[,]
                {
                    { 3, "", "Alice Johnson" },
                    { 4, "", "Bob Brown" }
                });

            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "Id", "Capacity", "Name", "SpotsLeft", "StartTime", "TrainerId", "TrainerName" },
                values: new object[,]
                {
                    { 3, 10, "Pilates", 7, new DateTime(2025, 6, 17, 10, 0, 0, 0, DateTimeKind.Utc), 3, "Jane Smith" },
                    { 4, 25, "Zumba", 25, new DateTime(2025, 6, 17, 10, 0, 0, 0, DateTimeKind.Utc), 4, "Bob Brown" },
                    { 5, 15, "Strength Training", 15, new DateTime(2025, 6, 18, 10, 0, 0, 0, DateTimeKind.Utc), 3, "Alice Johnson" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GymClasses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
