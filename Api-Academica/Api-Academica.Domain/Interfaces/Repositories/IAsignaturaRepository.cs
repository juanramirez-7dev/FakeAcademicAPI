using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IAsignaturaRepository
    {
        Task<IEnumerable<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task<Asignatura> CreateAsync(Asignatura entity);
        Task UpdateAsync(Asignatura entity);
        Task DeleteAsync(int id);

        Task<Asignatura?> GetByCodigoAsync(int planId, string codigo);
    }
}
