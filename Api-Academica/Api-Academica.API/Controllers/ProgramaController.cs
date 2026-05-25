using Api_Academica.API.DTOs.Request;
using Api_Academica.API.DTOs.Response;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Services;
using Api_Academica.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Academica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramaController : ControllerBase
    {

        private readonly IProgramaService _service;

        public ProgramaController(IProgramaService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProgramaResponseDTO>>> GetAll()
        {
            var programas = await _service.GetAllAsync();
            return Ok(programas.Select(p => new ProgramaResponseDTO
            {
                ProgramaId = p.ProgramaId,
                FacultadId = p.FacultadId,
                Nombre = p.Nombre,
                Codigo = p.Codigo,
                Nivel = p.Nivel,
                CreditosTotales = p.CreditosTotales,
                Semestres=p.Semestres,
            }));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramaResponseDTO>> GetById(int id)
        {
            try
            {
                var programa = await _service.GetByIdAsync(id);
                return Ok(new ProgramaResponseDTO
                {
                    ProgramaId = programa.ProgramaId,
                    FacultadId = programa.FacultadId,
                    Nombre = programa.Nombre,
                    Codigo = programa.Codigo,
                    Nivel = programa.Nivel,
                    CreditosTotales = programa.CreditosTotales,
                    Semestres = programa.Semestres,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProgramaResponseDTO>> Create(ProgramaRequestDTO dto)
        {
            try
            {
                var programa = new Programa
                {
                    
                    FacultadId = dto.FacultadId,
                    Nombre = dto.Nombre,
                    Codigo = dto.Codigo,
                    Nivel = dto.Nivel,
                    CreditosTotales = dto.CreditosTotales,
                    Semestres = dto.Semestres
                };
                var createdPrograma = await _service.CreateAsync(programa);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdPrograma.ProgramaId
                },
                new ProgramaResponseDTO
                {
                    ProgramaId = createdPrograma.ProgramaId,
                    FacultadId = createdPrograma.FacultadId,
                    Nombre = createdPrograma.Nombre,
                    Codigo = createdPrograma.Codigo,
                    Nivel = createdPrograma.Nivel,
                    CreditosTotales = createdPrograma.CreditosTotales,
                    Semestres = createdPrograma.Semestres,
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
        public async Task<ActionResult> Update(int id, ProgramaRequestDTO dto)
        {
            try
            {
                var programa= new Programa
                {
                    ProgramaId = id,
                    FacultadId = dto.FacultadId,
                    Nombre = dto.Nombre,
                    Codigo = dto.Codigo,
                    Nivel = dto.Nivel,
                    CreditosTotales = dto.CreditosTotales,
                    Semestres = dto.Semestres
                };
                await _service.UpdateAsync(programa, id);
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







    }
}
