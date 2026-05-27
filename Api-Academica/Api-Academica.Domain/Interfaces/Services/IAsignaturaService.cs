using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IAsignaturaService
    {

        Task<IEnumerable<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task<Asignatura> CreateAsync(Asignatura entity);
        Task UpdateAsync(Asignatura entity, int id);
        Task DeleteAsync(int id);
        
    }
}
