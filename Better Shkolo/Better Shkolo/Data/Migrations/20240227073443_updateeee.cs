using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Data.Migrations
{
    public partial class updateeee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentGradeId",
                table: "YearMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentGradeId",
                table: "TermMarks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_YearMarks_StudentGradeId",
                table: "YearMarks",
                column: "StudentGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TermMarks_StudentGradeId",
                table: "TermMarks",
                column: "StudentGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TermMarks_Grades_StudentGradeId",
                table: "TermMarks",
                column: "StudentGradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearMarks_Grades_StudentGradeId",
                table: "YearMarks",
                column: "StudentGradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TermMarks_Grades_StudentGradeId",
                table: "TermMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_YearMarks_Grades_StudentGradeId",
                table: "YearMarks");

            migrationBuilder.DropIndex(
                name: "IX_YearMarks_StudentGradeId",
                table: "YearMarks");

            migrationBuilder.DropIndex(
                name: "IX_TermMarks_StudentGradeId",
                table: "TermMarks");

            migrationBuilder.DropColumn(
                name: "StudentGradeId",
                table: "YearMarks");

            migrationBuilder.DropColumn(
                name: "StudentGradeId",
                table: "TermMarks");
        }
    }
}
