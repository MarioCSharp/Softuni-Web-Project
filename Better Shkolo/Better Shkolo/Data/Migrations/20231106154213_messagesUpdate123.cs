using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterShkolo.Data.Migrations
{
    public partial class messagesUpdate123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentToId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "SentToUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SentToUserId",
                table: "Messages",
                column: "SentToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SentToUserId",
                table: "Messages",
                column: "SentToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SentToUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SentToUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SentToUserId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "SentToId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
