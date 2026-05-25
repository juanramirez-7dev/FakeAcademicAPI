using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Response
{
    public class PeriodoAcademicoResponseDTO
    {
        public int PeriodoId { get; set; }
        public int Anio { get; set; }
        public int Semestre { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public EstadoPeriodoAcademico Estado { get; set; } = EstadoPeriodoAcademico.Abierto;
    }
}
