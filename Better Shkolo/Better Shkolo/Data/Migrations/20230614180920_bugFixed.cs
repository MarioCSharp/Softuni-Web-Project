using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Data.Migrations
{
    public partial class bugFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Subjects_SchoolId",
                table: "Marks");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_SubjectId",
                table: "Marks",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Subjects_SubjectId",
                table: "Marks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Subjects_SubjectId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_SubjectId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Subjects_SchoolId",
                table: "Marks",
                column: "SchoolId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
