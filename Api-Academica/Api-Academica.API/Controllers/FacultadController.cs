using Api_Academica.API.DTOs.Request;
using Api_Academica.API.DTOs.Response;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Api_Academica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadController : ControllerBase
    {
        private readonly IFacultadService _service;

        public FacultadController(IFacultadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultadResponseDTO>>> GetAll()
        {
            var facultades = await _service.GetAllAsync();
            return Ok(facultades.Select(f => new FacultadResponseDTO
            {
                Id = f.Id,
                Nombre = f.Nombre,
                Codigo = f.Codigo,
                Estado = f.Estado
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultadResponseDTO>> GetById(int id)
        {
            try
            {
                var facultad = await _service.GetByIdAsync(id);
                return Ok(new FacultadResponseDTO
                {
                    Id = facultad.Id,
                    Nombre = facultad.Nombre,
                    Codigo = facultad.Codigo,
                    Estado = facultad.Estado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FacultadResponseDTO>> Create(FacultadRequestDTO dto)
        {
            try
            {
                var facultad = new Facultad
                {
                    Nombre = dto.Nombre,
                    Codigo = dto.Codigo,
                };
                var createdFacultad = await _service.CreateAsync(facultad);
                return CreatedAtAction(nameof(GetById), new
                {
                    id = createdFacultad.Id
                },
                new FacultadResponseDTO
                {
                    Id = createdFacultad.Id,
                    Nombre = createdFacultad.Nombre,
                    Codigo = createdFacultad.Codigo,
                    Estado = createdFacultad.Estado
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
        public async Task<ActionResult> Update(int id,FacultadRequestDTO dto)
        {
            try
            { 
                var facultad = new Facultad
                {
                    Id = id,
                    Nombre = dto.Nombre,
                    Codigo = dto.Codigo,
                };
                await _service.UpdateAsync(facultad,id);
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

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> UpdateState(int id,EstadoFacultadRequestDTO dto)
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
