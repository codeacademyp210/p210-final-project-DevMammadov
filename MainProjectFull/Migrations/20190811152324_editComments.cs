using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class editComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderImage",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SenderSurname",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderImage",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderSurname",
                table: "Comments",
                nullable: true);
        }
    }
}
