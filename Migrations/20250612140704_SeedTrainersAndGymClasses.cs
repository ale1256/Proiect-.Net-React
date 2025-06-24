using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedTrainersAndGymClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.CreateTable(
                name: "GymClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TrainerName = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    SpotsLeft = table.Column<int>(type: "integer", nullable: false),
                    TrainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymClasses_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGymClasses",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    GymClassesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGymClasses", x => new { x.ClientsId, x.GymClassesId });
                    table.ForeignKey(
                        name: "FK_ClientGymClasses_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientGymClasses_GymClasses_GymClassesId",
                        column: x => x.GymClassesId,
                        principalTable: "GymClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Expertise", "FullName" },
                values: new object[,]
                {
                    { 1, "", "John Doe" },
                    { 2, "", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "Id", "Capacity", "Name", "SpotsLeft", "StartTime", "TrainerId", "TrainerName" },
                values: new object[,]
                {
                    { 1, 20, "HIIT", 20, new DateTime(2025, 6, 13, 10, 0, 0, 0, DateTimeKind.Utc), 1, "John Doe" },
                    { 2, 15, "Yoga", 15, new DateTime(2025, 6, 13, 10, 0, 0, 0, DateTimeKind.Utc), 2, "Jane Smith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientGymClasses_GymClassesId",
                table: "ClientGymClasses",
                column: "GymClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_GymClasses_TrainerId",
                table: "GymClasses",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientGymClasses");

            migrationBuilder.DropTable(
                name: "GymClasses");

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainerId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCourses",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    CoursesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCourses", x => new { x.ClientsId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_ClientCourses_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCourses_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCourses_CoursesId",
                table: "ClientCourses",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrainerId",
                table: "Courses",
                column: "TrainerId");
        }
    }
}
