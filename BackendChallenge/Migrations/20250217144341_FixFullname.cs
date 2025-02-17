using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendChallenge.Migrations
{
    /// <inheritdoc />
    public partial class FixFullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "Fullname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "User",
                newName: "Email");
        }
    }
}
