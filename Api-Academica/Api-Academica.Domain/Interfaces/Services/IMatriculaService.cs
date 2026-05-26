using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IMatriculaService
    {
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula?> GetByIdAsync(int id);
        Task<Matricula> CreateAsync(Matricula entity);
        Task UpdateAsync(Matricula entity, int id);
        Task UpdateStateAsync(int id, EstadoMatricula estado);
    }
}
