using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsernameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "dbo",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                schema: "dbo",
                table: "User");
        }
    }
}
