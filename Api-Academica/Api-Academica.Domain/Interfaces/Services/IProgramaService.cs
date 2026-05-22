using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IProgramaService
    {
        Task<IEnumerable<Programa>> GetAllAsync();
        Task<Programa?> GetByIdAsync(int id);
        Task<Programa> CreateAsync(Programa entity);
        Task UpdateAsync(Programa entity, int id);
        Task DeleteAsync(int id);
    }

}
