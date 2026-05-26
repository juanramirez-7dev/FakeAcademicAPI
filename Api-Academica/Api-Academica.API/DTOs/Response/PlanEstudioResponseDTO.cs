using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Response
{
    public class PlanEstudioResponseDTO
    {
        public int PlanId { get; set; }
        public int ProgramaId { get; set; }
        public string Version { get; set; } = string.Empty;
        public EstadoPlanEstudio Estado { get; set; }

    }
}
