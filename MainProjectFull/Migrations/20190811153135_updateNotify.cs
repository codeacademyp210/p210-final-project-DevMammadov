using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class updateNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Notify",
                newName: "Text");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Notify",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Notify");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Notify",
                newName: "Topic");
        }
    }
}
