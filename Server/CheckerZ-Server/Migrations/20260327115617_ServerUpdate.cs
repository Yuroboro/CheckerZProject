using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckerZ_Server.Migrations
{
    /// <inheritdoc />
    public partial class ServerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "Player",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
