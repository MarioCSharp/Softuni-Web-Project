using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Migrations
{
    public partial class erasmusApplicationUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "ErasmusApplications",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "ErasmusApplications");
        }
    }
}
