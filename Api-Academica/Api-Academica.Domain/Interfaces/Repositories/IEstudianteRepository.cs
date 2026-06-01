using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task<Estudiante?> GetByDocumentAsync(string document);

        Task<Estudiante> CreateAsync(Estudiante entity);

        Task DeleteAsync(int id);

        Task UpdateAsync(Estudiante entity);

        Task<IEnumerable<Estudiante>> GetByProgramaIdAsync(int id);
        Task<Estudiante?> GetByNumeroDocumentoAsync(string documento);
        Task<Estudiante?> GetByCorreoInstitucionalAsync(string correo);
        Task<Estudiante?> GetByCodigoEstudiantilAsync(string codigo);

    }
}
