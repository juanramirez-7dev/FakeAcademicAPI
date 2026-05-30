using Api_Academica.API.DTOs.Request;
using Api_Academica.API.DTOs.Response;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Academica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoAcademicoController : ControllerBase
    {

        private readonly IPeriodoAcademicoService _service;

        public PeriodoAcademicoController(IPeriodoAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodoAcademicoResponseDTO>>> GetAll()
        {
            var periodos = await _service.GetAllAsync();
            return Ok(periodos.Select(f => new PeriodoAcademicoResponseDTO
            {
               PeriodoId=f.PeriodoId,
               Anio=f.Anio,
               Semestre=f.Semestre,
               FechaInicio=f.FechaInicio,
               FechaFin=f.FechaFin,
               Estado=f.Estado,

            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodoAcademicoResponseDTO>> GetById(int id)
        {
            try
            {
                var periodo = await _service.GetByIdAsync(id);
                return Ok(new PeriodoAcademicoResponseDTO
                {
                    PeriodoId=periodo.PeriodoId,
                    Anio=periodo.Anio,
                    Semestre = periodo.Semestre,
                    FechaInicio = periodo.FechaInicio,
                    FechaFin = periodo.FechaFin,    
                    Estado = periodo.Estado,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PeriodoAcademicoResponseDTO>> Create(PeriodoAcademicoRequestDTO dto)
        {
            try
            {
                var periodo = new PeriodoAcademico
                {
                    Anio=dto.Anio,
                    Semestre=dto.Semestre,
                    FechaInicio=dto.FechaInicio,
                    FechaFin=dto.FechaFin,

                        
                };
                var createdPeriodoAcademico = await _service.CreateAsync(periodo);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdPeriodoAcademico.PeriodoId
                },
                new PeriodoAcademicoResponseDTO
                {
                    PeriodoId = createdPeriodoAcademico.PeriodoId,
                    Anio = createdPeriodoAcademico.Anio,
                    Semestre = createdPeriodoAcademico.Semestre,
                    FechaInicio = createdPeriodoAcademico.FechaInicio,
                    FechaFin = createdPeriodoAcademico.FechaFin,
                    Estado= createdPeriodoAcademico.Estado
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
        public async Task<ActionResult> Update(int id, PeriodoAcademicoRequestDTO dto)
        {
            try
            {
                var periodo = new PeriodoAcademico
                {
                    PeriodoId = id
               
                };
                await _service.UpdateAsync(periodo, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> UpdateState(int id, EstadoPeriodoAcademicoRequestDTO dto)
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
