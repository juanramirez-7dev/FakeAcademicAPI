namespace Api_Academica.API.DTOs.Response
{
    public class AsignaturaResponseDTO
    {
        public int AsignaturaId { get; set; }
        public int PlanId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int SemestreRecomendado { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
