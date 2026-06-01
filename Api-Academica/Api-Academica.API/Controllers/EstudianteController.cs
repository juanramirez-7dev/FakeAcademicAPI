using Api_Academica.API.DTOs.Request;
using Api_Academica.API.DTOs.Response;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Api_Academica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _service;

        public EstudianteController(IEstudianteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteResponseDTO>>> GetAll()
        {
            var estudiantes = await _service.GetAllAsync();
            return Ok(estudiantes.Select(e => new EstudianteResponseDTO
            {
                EstudianteId = e.EstudianteId,
                ProgramaId = e.ProgramaId,
                CodigoEstudiantil = e.CodigoEstudiantil,
                TipoDocumento = e.TipoDocumento,
                NumeroDocumento = e.NumeroDocumento,
                Nombres = e.Nombres,
                Apellidos = e.Apellidos,
                CorreoInstitucional = e.CorreoInstitucional,
                Telefono = e.Telefono,
                FechaIngreso = e.FechaIngreso,
                Estado = e.Estado





            }));
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<EstudianteResponseDTO>> GetById(int id)
        {
            try
            {
                var estudiante = await _service.GetByIdAsync(id);
                return Ok(new EstudianteResponseDTO
                {
                    EstudianteId = estudiante.EstudianteId,
                    ProgramaId = estudiante.ProgramaId,
                    CodigoEstudiantil = estudiante.CodigoEstudiantil,
                    TipoDocumento = estudiante.TipoDocumento,
                    NumeroDocumento = estudiante.NumeroDocumento,
                    Nombres = estudiante.Nombres,
                    Apellidos = estudiante.Apellidos,
                    CorreoInstitucional = estudiante.CorreoInstitucional,
                    Telefono = estudiante.Telefono,
                    FechaIngreso = estudiante.FechaIngreso,
                    Estado = estudiante.Estado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Document/{documento}")]

        public async Task<ActionResult<EstudianteResponseDTO>> GetByDocument(string documento)
        {
            try
            {
                var estudiante = await _service.GetByDocumentAsync(documento);
                return Ok(new EstudianteResponseDTO
                {
                    EstudianteId = estudiante.EstudianteId,
                    ProgramaId = estudiante.ProgramaId,
                    CodigoEstudiantil = estudiante.CodigoEstudiantil,
                    TipoDocumento = estudiante.TipoDocumento,
                    NumeroDocumento = estudiante.NumeroDocumento,
                    Nombres = estudiante.Nombres,
                    Apellidos = estudiante.Apellidos,
                    CorreoInstitucional = estudiante.CorreoInstitucional,
                    Telefono = estudiante.Telefono,
                    FechaIngreso = estudiante.FechaIngreso,
                    Estado = estudiante.Estado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteResponseDTO>> Create(EstudianteRequestDTO dto)
        {
            try
            {
                var estudiante = new Estudiante
                {

                    ProgramaId = dto.ProgramaId,
                    CodigoEstudiantil = dto.CodigoEstudiantil,
                    TipoDocumento = dto.TipoDocumento,
                    NumeroDocumento = dto.NumeroDocumento,
                    Nombres = dto.Nombres,
                    Apellidos = dto.Apellidos,
                    CorreoInstitucional = dto.CorreoInstitucional,
                    Telefono = dto.Telefono,
                    FechaIngreso = dto.FechaIngreso,



                };
                var createdEstudiante = await _service.CreateAsync(estudiante);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdEstudiante.EstudianteId,
                },
                new EstudianteResponseDTO
                {
                    EstudianteId = createdEstudiante.EstudianteId,
                    ProgramaId = createdEstudiante.ProgramaId,
                    CodigoEstudiantil = createdEstudiante.CodigoEstudiantil,
                    TipoDocumento = createdEstudiante.TipoDocumento,
                    NumeroDocumento = createdEstudiante.NumeroDocumento,
                    Nombres = createdEstudiante.Nombres,
                    Apellidos = createdEstudiante.Apellidos,
                    CorreoInstitucional = createdEstudiante.CorreoInstitucional,
                    Telefono = createdEstudiante.Telefono,
                    FechaIngreso = createdEstudiante.FechaIngreso,
                    Estado = createdEstudiante.Estado,

                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPut("{id}")]

        public async Task<ActionResult> Update(int id, EstudianteRequestDTO dto)
        {
            try
            {
                var estudiante = new Estudiante
                {
                    EstudianteId = id,
                    ProgramaId = dto.ProgramaId,
                    CodigoEstudiantil = dto.CodigoEstudiantil,
                    TipoDocumento = dto.TipoDocumento,
                    NumeroDocumento = dto.NumeroDocumento,
                    Nombres = dto.Nombres,
                    Apellidos = dto.Apellidos,
                    CorreoInstitucional = dto.CorreoInstitucional,
                    Telefono = dto.Telefono,

                };
                await _service.UpdateAsync(estudiante, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> UpdateState(int id, EstadoAcademicoEstudianteRequestDTO dto)
        {
            try
            {
                await _service.UpdateStateAsync(id, dto.Estado);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




































    }

}
