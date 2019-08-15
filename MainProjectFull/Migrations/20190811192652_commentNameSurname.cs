using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class commentNameSurname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Notify",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderSurname",
                table: "Notify",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Notify");

            migrationBuilder.DropColumn(
                name: "SenderSurname",
                table: "Notify");
        }
    }
}
