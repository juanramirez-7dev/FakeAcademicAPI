using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula?> GetByIdAsync(int id);
        Task<Matricula> CreateAsync(Matricula entity);
        Task UpdateAsync(Matricula entity);
      
        Task<IEnumerable<Matricula>> GetByEstudianteIdAsync(int id);

        Task<Matricula?> GetByEstudiantePeriodoAsync(int estudianteId, int periodoId);

        
       



    }
}
