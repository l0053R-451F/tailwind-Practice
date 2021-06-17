using Microsoft.EntityFrameworkCore.Migrations;

namespace Profile.Infrastructure.Migrations
{
    public partial class UserIdInPersonProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PersonProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PersonProfiles");
        }
    }
}
