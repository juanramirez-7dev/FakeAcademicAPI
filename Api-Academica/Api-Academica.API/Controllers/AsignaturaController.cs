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
    public class AsignaturaController : ControllerBase
    {
        private readonly IAsignaturaService _service;

        public AsignaturaController(IAsignaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignaturaResponseDTO>>> GetAll()
        {
            var asignaturas = await _service.GetAllAsync();
            return Ok(asignaturas.Select(f => new AsignaturaResponseDTO
            {
                AsignaturaId = f.AsignaturaId,
                PlanId = f.PlanId,
                Codigo = f.Codigo,
                Tipo = f.Tipo,
                Nombre = f.Nombre,
                SemestreRecomendado = f.SemestreRecomendado,
                Creditos = f.Creditos,

            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AsignaturaResponseDTO>> GetById(int id)
        {
            try
            {
                var asignatura = await _service.GetByIdAsync(id);
                return Ok(new AsignaturaResponseDTO
                {
                    AsignaturaId = asignatura.AsignaturaId,
                    PlanId = asignatura.PlanId,
                    Codigo = asignatura.Codigo,
                    Tipo = asignatura.Tipo,
                    Nombre = asignatura.Nombre,
                    SemestreRecomendado = asignatura.SemestreRecomendado,
                    Creditos = asignatura.Creditos,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AsignaturaResponseDTO>> Create(AsignaturaRequestDTO dto)
        {
            try
            {
                var asignatura = new Asignatura
                {
                    PlanId = dto.PlanId,
                    Codigo = dto.Codigo,
                    Tipo = dto.Tipo,
                    Nombre = dto.Nombre,
                    SemestreRecomendado = dto.SemestreRecomendado,
                    Creditos = dto.Creditos,
                };
                var createdAsignatura = await _service.CreateAsync(asignatura);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdAsignatura.AsignaturaId
                },
                new AsignaturaResponseDTO
                {
                    AsignaturaId = createdAsignatura.AsignaturaId,
                    PlanId = createdAsignatura.PlanId,
                    Codigo = createdAsignatura.Codigo,
                    Tipo = createdAsignatura.Tipo,
                    Nombre = createdAsignatura.Nombre,
                    SemestreRecomendado = createdAsignatura.SemestreRecomendado,
                    Creditos = createdAsignatura.Creditos,

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
        public async Task<ActionResult> Update(int id, AsignaturaRequestDTO dto)
        {
            try
            {
                var asignatura = new Asignatura
                {
                    AsignaturaId = id,
                    Tipo = dto.Tipo,
                    Nombre = dto.Nombre,
                    SemestreRecomendado = dto.SemestreRecomendado,
                    Creditos = dto.Creditos,
                };
                await _service.UpdateAsync(asignatura, id);
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
