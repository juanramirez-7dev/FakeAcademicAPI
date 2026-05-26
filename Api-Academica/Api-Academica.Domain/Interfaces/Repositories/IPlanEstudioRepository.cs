using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IPlanEstudioRepository
    {

        Task<IEnumerable<PlanEstudio>> GetAllAsync();
        Task<PlanEstudio?> GetByIdAsync(int id);

        Task<PlanEstudio> CreateAsync(PlanEstudio entity);

        Task UpdateAsync(PlanEstudio entity);

        Task<IEnumerable<PlanEstudio>> GetByProgramaIdAsync(int id);
        
    }
}
