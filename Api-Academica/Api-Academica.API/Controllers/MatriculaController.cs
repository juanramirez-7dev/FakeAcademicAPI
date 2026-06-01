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
    public class MatriculaController : ControllerBase
    {

        private readonly IMatriculaService _service;

        public MatriculaController(IMatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculaResponseDTO>>> GetAll()
        {
            var matriculas = await _service.GetAllAsync();
            return Ok(matriculas.Select(f => new MatriculaResponseDTO
            {
                MatriculaId = f.MatriculaId,
                PeriodoId = f.PeriodoId,
                EstudianteId = f.EstudianteId,
                FechaMatricula=f.FechaMatricula,
                Estado = f.Estado,

            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatriculaResponseDTO>> GetById(int id)
        {
            try
            {
                var matricula = await _service.GetByIdAsync(id);
                return Ok(new MatriculaResponseDTO
                {
                    MatriculaId = matricula.MatriculaId,
                    PeriodoId=matricula.PeriodoId,
                    EstudianteId=matricula.EstudianteId,
                    FechaMatricula=matricula.FechaMatricula,
                    Estado = matricula.Estado,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MatriculaResponseDTO>> Create(MatriculaRequestDTO dto)
        {
            try
            {
                var matricula = new Matricula
                {
                    EstudianteId=dto.EstudianteId,
                    PeriodoId = dto.PeriodoId,
                    FechaMatricula=dto.FechaMatricula,

                };
                var createdMatricula = await _service.CreateAsync(matricula);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdMatricula.MatriculaId
                },
                new MatriculaResponseDTO
                {
                    MatriculaId = createdMatricula.MatriculaId,
                    EstudianteId = createdMatricula.EstudianteId,
                    PeriodoId = createdMatricula.PeriodoId,
                    FechaMatricula = createdMatricula.FechaMatricula,
                    Estado = createdMatricula.Estado
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
        public async Task<ActionResult> Update(int id, MatriculaRequestDTO dto)
        {
            try
            {
                var matricula = new Matricula
                {
                    MatriculaId = id

                };
                await _service.UpdateAsync(matricula, id);
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
        public async Task<ActionResult> UpdateState(int id, EstadoMatriculaRequestDTO dto)
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
