using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProjectFull.Migrations
{
    public partial class setnullabletoprofileidinnotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SenderProfileId",
                table: "Notify",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SenderProfileId",
                table: "Notify",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
