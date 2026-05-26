using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Response
{
    public class MatriculaResponseDTO
    {

        public int MatriculaId { get; set; }
        public int EstudianteId { get; set; }
        public int PeriodoId { get; set; }
        public DateOnly FechaMatricula { get; set; }
        public EstadoMatricula Estado { get; set; } = EstadoMatricula.Activa;
        
    }
}
