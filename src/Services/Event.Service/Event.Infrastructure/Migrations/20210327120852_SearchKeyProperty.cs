using Microsoft.EntityFrameworkCore.Migrations;

namespace Event.Infrastructure.Migrations
{
    public partial class SearchKeyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchKey",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SearchKey",
                table: "Events",
                column: "SearchKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Events_SearchKey",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SearchKey",
                table: "Events");
        }
    }
}
