using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Academica.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HistorialAcademico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialAcademicos",
                columns: table => new
                {
                    HistorialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    NotaFinal = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CreditosAprobados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAcademicos", x => x.HistorialId);
                    table.ForeignKey(
                        name: "FK_HistorialAcademicos_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "AsignaturaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialAcademicos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialAcademicos_PeriodosAcademicos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "PeriodosAcademicos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademicos_AsignaturaId",
                table: "HistorialAcademicos",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademicos_EstudianteId_AsignaturaId_PeriodoId",
                table: "HistorialAcademicos",
                columns: new[] { "EstudianteId", "AsignaturaId", "PeriodoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademicos_PeriodoId",
                table: "HistorialAcademicos",
                column: "PeriodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialAcademicos");
        }
    }
}
