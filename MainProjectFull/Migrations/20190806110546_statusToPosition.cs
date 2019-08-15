using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class statusToPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Veryfied",
                table: "Position",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Veryfied",
                table: "Position");
        }
    }
}
