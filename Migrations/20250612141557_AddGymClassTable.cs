using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGymClassTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GymClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    TrainerName = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    SpotsLeft = table.Column<int>(nullable: false),
                    TrainerId = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "GymClasses",
                columns: new[] { "Id", "Capacity", "Name", "SpotsLeft", "StartTime", "TrainerId", "TrainerName" },
                values: new object[,]
                {
            { 1, 20, "HIIT", 20, new DateTime(2025, 6, 13, 10, 0, 0, DateTimeKind.Utc), 1, "John Doe" },
            { 2, 15, "Yoga", 15, new DateTime(2025, 6, 13, 10, 0, 0, DateTimeKind.Utc), 2, "Jane Smith" }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymClasses");
        }

    }
}
