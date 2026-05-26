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
    public class PlanEstudioController : ControllerBase
    {
        private readonly IPlanEstudioService _service;

        public PlanEstudioController(IPlanEstudioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanEstudioResponseDTO>>> GetAll()
        {
            var planEstudios = await _service.GetAllAsync();
            return Ok(planEstudios.Select(e => new PlanEstudioResponseDTO
            {
               
                PlanId = e.PlanId,
                ProgramaId=e.PlanId,
                Version = e.Version,
                Estado=e.Estado


            }));
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<PlanEstudioResponseDTO>> GetById(int id)
        {
            try
            {
                var planEstudio = await _service.GetByIdAsync(id);
                return Ok(new PlanEstudioResponseDTO
                {
                    //EstudianteId = estudiante.EstudianteId,
                    PlanId=planEstudio.PlanId,
                    ProgramaId = planEstudio.PlanId,
                    Version=planEstudio.Version,
                    Estado  = planEstudio.Estado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlanEstudioResponseDTO>> Create(PlanEstudioRequestDTO dto)
        {
            try
            {
                var planEstudio = new PlanEstudio
                {

                    ProgramaId = dto.ProgramaId,
                    Version = dto.Version,
                    



                };
                var createdPlanEstudio = await _service.CreateAsync(planEstudio);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdPlanEstudio.PlanId,
                },
                new PlanEstudioResponseDTO
                {
                    PlanId = createdPlanEstudio.PlanId,
                    ProgramaId = createdPlanEstudio.PlanId,
                    Version = createdPlanEstudio.Version,
                    Estado = createdPlanEstudio.Estado

                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPut("{id}")]

        public async Task<ActionResult> Update(int id, PlanEstudioRequestDTO dto)
        {
            try
            {
                var planEstudio = new PlanEstudio
                {
                    PlanId = id,
                   

                };
                await _service.UpdateAsync(planEstudio, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> UpdateState(int id, EstadoPlanEstudioRequestDTO dto)
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

