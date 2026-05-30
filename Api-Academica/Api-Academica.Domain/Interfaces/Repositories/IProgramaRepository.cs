using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IProgramaRepository
    {
        Task<IEnumerable<Programa>> GetAllAsync();
        Task<Programa?> GetByIdAsync(int id);

        Task<Programa> CreateAsync(Programa entity);

        Task DeleteAsync(int id);

        Task UpdateAsync(Programa entity);

        Task<Programa?> GetByNameAsync(string name);

        Task<Programa?> GetByCodeAsync(string code);

        Task<IEnumerable<Programa>> GetByFacultadIdAsync(int id);


    }
}
