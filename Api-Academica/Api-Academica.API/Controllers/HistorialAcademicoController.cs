using Api_Academica.API.DTOs.Request;
using Api_Academica.API.DTOs.Response;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Academica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialAcademicoController : ControllerBase
    {
        private readonly IHistorialAcademicoService _service;

        public HistorialAcademicoController(IHistorialAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialAcademicoResponseDTO>>> GetAll()
        {
            var historiales = await _service.GetAllAsync();

            return Ok(historiales.Select(h => new HistorialAcademicoResponseDTO
            {
                HistorialId = h.HistorialId,
                EstudianteId = h.EstudianteId,
                AsignaturaId = h.AsignaturaId,
                PeriodoId = h.PeriodoId,
                NotaFinal = h.NotaFinal,
                CreditosAprobados = h.CreditosAprobados,
                Estado = h.Estado
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialAcademicoResponseDTO>> GetById(int id)
        {
            try
            {
                var historial = await _service.GetByIdAsync(id);

                return Ok(new HistorialAcademicoResponseDTO
                {
                    HistorialId = historial.HistorialId,
                    EstudianteId = historial.EstudianteId,
                    AsignaturaId = historial.AsignaturaId,
                    PeriodoId = historial.PeriodoId,
                    NotaFinal = historial.NotaFinal,
                    CreditosAprobados = historial.CreditosAprobados,
                    Estado = historial.Estado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<HistorialAcademicoResponseDTO>> Create(
            HistorialAcademicoRequestDTO dto)
        {
            try
            {
                var historial = new HistorialAcademico
                {
                    EstudianteId = dto.EstudianteId,
                    AsignaturaId = dto.AsignaturaId,
                    PeriodoId = dto.PeriodoId,
                    NotaFinal = dto.NotaFinal,
                    CreditosAprobados = dto.CreditosAprobados,
                   
                };

                var createdHistorial = await _service.CreateAsync(historial);

                return CreatedAtAction(nameof(GetById),
                    new { id = createdHistorial.HistorialId },
                    new HistorialAcademicoResponseDTO
                    {
                        HistorialId = createdHistorial.HistorialId,
                        EstudianteId = createdHistorial.EstudianteId,
                        AsignaturaId = createdHistorial.AsignaturaId,
                        PeriodoId = createdHistorial.PeriodoId,
                        NotaFinal = createdHistorial.NotaFinal,
                        CreditosAprobados = createdHistorial.CreditosAprobados,
                        Estado = createdHistorial.Estado
                    });
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            int id,
            HistorialAcademicoRequestDTO dto)
        {
            try
            {
                var historial = new HistorialAcademico
                {
                    HistorialId = id,
                    EstudianteId = dto.EstudianteId,
                    AsignaturaId = dto.AsignaturaId,
                    PeriodoId = dto.PeriodoId,
                    NotaFinal = dto.NotaFinal,
                    CreditosAprobados = dto.CreditosAprobados,
                    
                };

                await _service.UpdateAsync(historial, id);

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
        public async Task<ActionResult> UpdateState(
            int id,
            EstadoHistorialAcademico estado)
        {
            try
            {
                await _service.UpdateStateAsync(id, estado);
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
        [HttpGet("{estudianteId}/HistorialAcademico")]
        public async Task<ActionResult<List<HistorialAcademicoResponseDTO>>> GetHistorialAcademico(int estudianteId)
        {
            try
            {
                var historial = await _service.GetByEstudianteIdAsync(estudianteId);
                var response = historial.Select(h => new HistorialAcademicoResponseDTO
                {
                    HistorialId = h.HistorialId,
                    EstudianteId = h.EstudianteId,
                    AsignaturaId = h.AsignaturaId,
                    PeriodoId = h.PeriodoId,
                    NotaFinal = h.NotaFinal,
                    CreditosAprobados = h.CreditosAprobados,
                    Estado = h.Estado
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Document/{documento}/HistorialAcademico")]
        public async Task<ActionResult<IEnumerable<HistorialAcademicoResponseDTO>>> GetHistorialAcademicoByDocument(string documento)
        {
            try
            {
                var historial = await _service.GetByDocumentAsync(documento);
                var response = historial.Select(h => new HistorialAcademicoResponseDTO
                {
                    HistorialId = h.HistorialId,
                    EstudianteId = h.EstudianteId,
                    AsignaturaId = h.AsignaturaId,
                    PeriodoId = h.PeriodoId,
                    NotaFinal = h.NotaFinal,
                    CreditosAprobados = h.CreditosAprobados,
                    Estado = h.Estado
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
    

