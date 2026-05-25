using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IPeriodoAcademicoService
    {
        Task<IEnumerable<PeriodoAcademico>> GetAllAsync();
        Task<PeriodoAcademico?> GetByIdAsync(int id);
        Task<PeriodoAcademico> CreateAsync(PeriodoAcademico entity);
        Task UpdateAsync(PeriodoAcademico entity, int id);
        Task UpdateStateAsync(int id, EstadoPeriodoAcademico estado);
    }
}
