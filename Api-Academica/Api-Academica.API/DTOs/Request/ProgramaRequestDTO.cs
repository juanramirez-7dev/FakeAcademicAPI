namespace Api_Academica.API.DTOs.Request
{
    public class ProgramaRequestDTO
    {
      
        public int FacultadId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int CreditosTotales { get; set; }
        public int Semestres { get; set; }
       
    }
}
