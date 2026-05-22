namespace Api_Academica.API.DTOs.Response
{
    public class ProgramaResponseDTO
    {
        public int ProgramaId { get; set; }
        public int FacultadId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int CreditosTotales { get; set; }
        public int Semestres { get; set; }
       
    }
}
