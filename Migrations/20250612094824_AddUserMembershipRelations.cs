using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMembershipRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMemberships");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "UserMemberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_ClientId",
                table: "UserMemberships",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemberships_Clients_ClientId",
                table: "UserMemberships",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMemberships_Clients_ClientId",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_ClientId",
                table: "UserMemberships");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "UserMemberships");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserMemberships",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
