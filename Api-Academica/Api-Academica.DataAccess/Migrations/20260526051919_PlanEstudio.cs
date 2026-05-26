using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Academica.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PlanEstudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanesEstudio",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramaId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEstudio", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_PlanesEstudio_Programas_ProgramaId",
                        column: x => x.ProgramaId,
                        principalTable: "Programas",
                        principalColumn: "ProgramaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanesEstudio_ProgramaId_Version",
                table: "PlanesEstudio",
                columns: new[] { "ProgramaId", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanesEstudio");
        }
    }
}
