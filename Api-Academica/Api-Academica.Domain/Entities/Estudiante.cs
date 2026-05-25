using Api_Academica.Domain.Enums;

namespace Api_Academica.Domain.Entities
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public int ProgramaId { get; set; }
        public string CodigoEstudiantil { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CorreoInstitucional { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly FechaIngreso { get; set; }
        public EstadoAcademicoEstudiante Estado { get; set; } = EstadoAcademicoEstudiante.Activo;
        public Programa Programa { get; set; } = null!;
    }
}
