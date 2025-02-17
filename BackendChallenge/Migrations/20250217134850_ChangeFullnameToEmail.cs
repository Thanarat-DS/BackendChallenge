using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendChallenge.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFullnameToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "User",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "FullName");
        }
    }
}
