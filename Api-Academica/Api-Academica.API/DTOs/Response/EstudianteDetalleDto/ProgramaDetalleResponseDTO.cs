namespace Api_Academica.API.DTOs.Response.EstudianteCompletoDto
{
    public class ProgramaDetalleResponseDTO
    {
        public int ProgramaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;

        public FacultadDetalleResponseDTO Facultad { get; set; } = null!;
    }
}
