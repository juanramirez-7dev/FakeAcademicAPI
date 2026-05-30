using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Response
{
    public class HistorialAcademicoResponseDTO
    {
        public int HistorialId { get; set; }

        public int EstudianteId { get; set; }
        public int AsignaturaId { get; set; }
        public int PeriodoId { get; set; }

        public decimal NotaFinal { get; set; }

        public EstadoHistorialAcademico Estado { get; set; }

        public int CreditosAprobados { get; set; }
    }
}
