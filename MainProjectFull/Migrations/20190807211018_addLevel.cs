using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class addLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "UsersLanguages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "UsersLanguages");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Languages",
                nullable: true);
        }
    }
}
