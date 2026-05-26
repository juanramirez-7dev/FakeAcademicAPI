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

        public PlanEstudioService(IPlanEstudioRepository repository)
        {
            _repository = repository;
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
           
            return await _repository.CreateAsync(entity);
        }
        public async Task UpdateAsync(PlanEstudio entity, int id)
        {
            PlanEstudio? planEstudio = await _repository.GetByIdAsync(id);
            if (planEstudio == null)
            {
                throw new KeyNotFoundException($"No se encontro un plan de estudio con el id:{id}");
            }
            
            await _repository.UpdateAsync(planEstudio);
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

