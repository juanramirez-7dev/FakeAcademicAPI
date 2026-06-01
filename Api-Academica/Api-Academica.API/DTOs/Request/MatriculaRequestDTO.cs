using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Request
{
    public class MatriculaRequestDTO
    {
        public int EstudianteId { get; set; }
        public int PeriodoId { get; set; }
        public DateOnly FechaMatricula { get; set; }
        
       
    }
}
