using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Data.Migrations
{
    public partial class updateOnYearAndTermMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "YearMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "TermMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_YearMarks_SubjectId",
                table: "YearMarks",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TermMarks_SubjectId",
                table: "TermMarks",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TermMarks_Subjects_SubjectId",
                table: "TermMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearMarks_Subjects_SubjectId",
                table: "YearMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TermMarks_Subjects_SubjectId",
                table: "TermMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_YearMarks_Subjects_SubjectId",
                table: "YearMarks");

            migrationBuilder.DropIndex(
                name: "IX_YearMarks_SubjectId",
                table: "YearMarks");

            migrationBuilder.DropIndex(
                name: "IX_TermMarks_SubjectId",
                table: "TermMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "YearMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "TermMarks");
        }
    }
}
