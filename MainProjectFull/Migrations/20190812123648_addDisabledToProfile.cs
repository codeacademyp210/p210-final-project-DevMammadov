using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class addDisabledToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Profile",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Profile");
        }
    }
}
