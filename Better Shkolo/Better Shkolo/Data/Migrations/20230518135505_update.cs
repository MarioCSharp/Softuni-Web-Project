using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Schools_SchoolId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Subjects_SubjectId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Schools_SchoolId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Schools_SchoolId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Student_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Schools_SchoolId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Teachers_TeacherId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_DirectorId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Schools_SchoolId",
                table: "Absencess",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Subjects_SubjectId",
                table: "Absencess",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Schools_SchoolId",
                table: "Grades",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherId",
                table: "Grades",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Schools_SchoolId",
                table: "Marks",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Student_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Schools_SchoolId",
                table: "Reviews",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Teachers_TeacherId",
                table: "Reviews",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_DirectorId",
                table: "Schools",
                column: "DirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Schools_SchoolId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Subjects_SubjectId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Schools_SchoolId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Schools_SchoolId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Student_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Schools_SchoolId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Teachers_TeacherId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_DirectorId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Schools_SchoolId",
                table: "Absencess",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Subjects_SubjectId",
                table: "Absencess",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Schools_SchoolId",
                table: "Grades",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherId",
                table: "Grades",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Schools_SchoolId",
                table: "Marks",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Student_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Schools_SchoolId",
                table: "Reviews",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Teachers_TeacherId",
                table: "Reviews",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_DirectorId",
                table: "Schools",
                column: "DirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
