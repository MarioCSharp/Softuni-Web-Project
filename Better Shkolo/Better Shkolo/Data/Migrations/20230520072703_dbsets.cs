using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterShkolo.Data.Migrations
{
    public partial class dbsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Student_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_AspNetUsers_UserId",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Student_StudentId",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_UserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Grades_GradeId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Parent_ParentId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Schools_SchoolId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Teachers_GradeTeacherId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Teachers_TeacherId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parent",
                table: "Parent");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Parent",
                newName: "Parents");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UserId",
                table: "Students",
                newName: "IX_Students_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_TeacherId",
                table: "Students",
                newName: "IX_Students_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_SchoolId",
                table: "Students",
                newName: "IX_Students_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_ParentId",
                table: "Students",
                newName: "IX_Students_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_GradeTeacherId",
                table: "Students",
                newName: "IX_Students_GradeTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_GradeId",
                table: "Students",
                newName: "IX_Students_GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_UserId",
                table: "Parents",
                newName: "IX_Parents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_StudentId",
                table: "Parents",
                newName: "IX_Parents_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parents",
                table: "Parents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Students_StudentId",
                table: "Absencess",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Students_StudentId",
                table: "Parents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Students_StudentId",
                table: "Reviews",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_GradeTeacherId",
                table: "Students",
                column: "GradeTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absencess_Students_StudentId",
                table: "Absencess");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Students_StudentId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Students_StudentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_GradeTeacherId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_TeacherId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parents",
                table: "Parents");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Parents",
                newName: "Parent");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UserId",
                table: "Student",
                newName: "IX_Student_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_TeacherId",
                table: "Student",
                newName: "IX_Student_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolId",
                table: "Student",
                newName: "IX_Student_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ParentId",
                table: "Student",
                newName: "IX_Student_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GradeTeacherId",
                table: "Student",
                newName: "IX_Student_GradeTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GradeId",
                table: "Student",
                newName: "IX_Student_GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_UserId",
                table: "Parent",
                newName: "IX_Parent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_StudentId",
                table: "Parent",
                newName: "IX_Parent_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parent",
                table: "Parent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absencess_Student_StudentId",
                table: "Absencess",
                column: "StudentId",
                principalTable: "Student",
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
                name: "FK_Parent_AspNetUsers_UserId",
                table: "Parent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Student_StudentId",
                table: "Parent",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Student_StudentId",
                table: "Reviews",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_UserId",
                table: "Student",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Grades_GradeId",
                table: "Student",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Parent_ParentId",
                table: "Student",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Schools_SchoolId",
                table: "Student",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Teachers_GradeTeacherId",
                table: "Student",
                column: "GradeTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Teachers_TeacherId",
                table: "Student",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
