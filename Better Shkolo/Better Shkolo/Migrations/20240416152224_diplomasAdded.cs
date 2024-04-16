using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better_Shkolo.Migrations
{
    public partial class diplomasAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FabricNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    YearRegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducationForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diplomas_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_SchoolId",
                table: "Diplomas",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diplomas");
        }
    }
}
