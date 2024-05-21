using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Migrations
{
    public partial class testNotiUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationSend",
                table: "Tests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationSend",
                table: "Tests");
        }
    }
}
