namespace Api_Academica.API.DTOs.Request
{
    public class PeriodoAcademicoRequestDTO
    {
        public int Anio { get; set; }
        public int Semestre { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}
