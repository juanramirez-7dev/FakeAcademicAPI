using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Academica.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Matricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    FechaMatricula = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.MatriculaId);
                    table.ForeignKey(
                        name: "FK_Matriculas_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_PeriodosAcademicos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "PeriodosAcademicos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_EstudianteId_PeriodoId",
                table: "Matriculas",
                columns: new[] { "EstudianteId", "PeriodoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_PeriodoId",
                table: "Matriculas",
                column: "PeriodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriculas");
        }
    }
}
