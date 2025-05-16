using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalProfileServiceProvider.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UserProfiles");
        }
    }
}
