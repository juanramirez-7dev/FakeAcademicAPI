using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Request
{
    public class HistorialAcademicoRequestDTO
    {
        public int EstudianteId { get; set; }
        public int AsignaturaId { get; set; }
        public int PeriodoId { get; set; }

        public decimal NotaFinal { get; set; }

        public int CreditosAprobados { get; set; }

       


    }
}
