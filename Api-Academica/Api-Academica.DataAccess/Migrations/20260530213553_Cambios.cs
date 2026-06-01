using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Academica.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Cambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Programas_Codigo",
                table: "Programas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodosAcademicos_Anio_Semestre",
                table: "PeriodosAcademicos",
                columns: new[] { "Anio", "Semestre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CodigoEstudiantil",
                table: "Estudiantes",
                column: "CodigoEstudiantil",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CorreoInstitucional",
                table: "Estudiantes",
                column: "CorreoInstitucional",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_NumeroDocumento",
                table: "Estudiantes",
                column: "NumeroDocumento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Programas_Codigo",
                table: "Programas");

            migrationBuilder.DropIndex(
                name: "IX_PeriodosAcademicos_Anio_Semestre",
                table: "PeriodosAcademicos");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_CodigoEstudiantil",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_CorreoInstitucional",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_NumeroDocumento",
                table: "Estudiantes");
        }
    }
}
