using Api_Academica.Domain.Enums;

namespace Api_Academica.API.DTOs.Response
{
    public class FacultadResponseDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public EstadoFacultad Estado { get; set; }
    }
}
