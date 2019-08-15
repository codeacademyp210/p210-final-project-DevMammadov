using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class removeUsersDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
