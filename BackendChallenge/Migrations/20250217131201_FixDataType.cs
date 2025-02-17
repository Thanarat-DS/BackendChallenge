using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendChallenge.Migrations
{
    /// <inheritdoc />
    public partial class FixDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginRequest");

            migrationBuilder.DropTable(
                name: "RegisterRequest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginRequest",
                columns: table => new
                {
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RegisterRequest",
                columns: table => new
                {
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
