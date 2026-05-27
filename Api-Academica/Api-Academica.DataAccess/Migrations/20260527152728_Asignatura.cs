using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Academica.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Asignatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    SemestreRecomendado = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.AsignaturaId);
                    table.ForeignKey(
                        name: "FK_Asignaturas_PlanesEstudio_PlanId",
                        column: x => x.PlanId,
                        principalTable: "PlanesEstudio",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaturas_PlanId_Codigo",
                table: "Asignaturas",
                columns: new[] { "PlanId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaturas");
        }
    }
}
