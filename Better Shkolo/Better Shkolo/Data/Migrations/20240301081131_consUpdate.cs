using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Data.Migrations
{
    public partial class consUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Consultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Consultations");
        }
    }
}
