using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IFacultadRepository
    {
        Task<IEnumerable<Facultad>> GetAllAsync();
        Task<Facultad?> GetByIdAsync(int id);
        Task<Facultad> CreateAsync(Facultad entity);
        Task UpdateAsync(Facultad entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Facultad?> GetByNameAsync(string name);
    }
}
