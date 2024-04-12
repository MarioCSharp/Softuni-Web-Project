using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterShkolo.Data.Migrations
{
    public partial class userUppp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorPhone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorPhone",
                table: "AspNetUsers");
        }
    }
}
