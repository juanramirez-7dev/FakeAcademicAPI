using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class PlanEstudioService:IPlanEstudioService
    {
        private readonly IPlanEstudioRepository _repository;
        private readonly IProgramaRepository _programaRepository;

        public PlanEstudioService(IPlanEstudioRepository repository, IProgramaRepository programaRepository)
        {
            _repository = repository;
            _programaRepository = programaRepository;
        }


        public async Task<IEnumerable<PlanEstudio>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<PlanEstudio?> GetByIdAsync(int id)
        {
            PlanEstudio? planEstudio = await _repository.GetByIdAsync(id);
            if (planEstudio == null)
            {
                throw new KeyNotFoundException($"No se encontro un plan de estudio con el id:{id}");
            }
            return planEstudio;
        }
        public async Task<PlanEstudio> CreateAsync(PlanEstudio entity)
        {
            var programa = await _programaRepository.GetByIdAsync(entity.ProgramaId);

            if (programa == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un programa con el id: {entity.ProgramaId}");
            }
            if (string.IsNullOrWhiteSpace(entity.Version))
            {
                throw new InvalidOperationException(
                    "La versión del plan de estudio es obligatoria");
            }
            entity.Estado = EstadoPlanEstudio.Activo;
            return await _repository.CreateAsync(entity);
        }
        public async Task UpdateAsync(PlanEstudio entity, int id)
        {
            PlanEstudio? planEstudio = await _repository.GetByIdAsync(id);
            if (planEstudio == null)
            {
                throw new KeyNotFoundException($"No se encontro un plan de estudio con el id:{id}");
            }
                throw new InvalidOperationException(
             "No se permite modificar un plan de estudio. Solo se puede actualizar su estado.");
        }

       
        

        public async Task UpdateStateAsync(int id, EstadoPlanEstudio estado)
        {
            PlanEstudio? planEstudio = await _repository.GetByIdAsync(id);
            if (planEstudio == null)
            {
                throw new KeyNotFoundException($"No se encontro un plan de estudio con el id:{id}");
            }
            if (planEstudio.Estado==estado)
            {
                throw new InvalidOperationException($"El plan de estudio ya se encuentra con el estado {estado}");
            }
            planEstudio.Estado = estado;
            await _repository.UpdateAsync(planEstudio);
        }
    }



}

