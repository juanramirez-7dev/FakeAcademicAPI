using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IPeriodoAcademicoRepository
    {
        Task<IEnumerable<PeriodoAcademico>> GetAllAsync();
        Task<PeriodoAcademico?> GetByIdAsync(int id);
        Task<PeriodoAcademico> CreateAsync(PeriodoAcademico entity);

        Task UpdateAsync(PeriodoAcademico entity);
    }
}
