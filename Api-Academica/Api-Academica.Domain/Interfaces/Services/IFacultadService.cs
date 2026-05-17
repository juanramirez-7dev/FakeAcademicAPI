using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IFacultadService
    {
        Task<IEnumerable<Facultad>> GetAllAsync();
        Task<Facultad?> GetByIdAsync(int id);
        Task<Facultad> CreateAsync(Facultad entity);
        Task UpdateAsync(Facultad entity,int id);
        Task DeleteAsync(int id);
        Task UpdateStateAsync(int id,EstadoFacultad estado);
    }
}
