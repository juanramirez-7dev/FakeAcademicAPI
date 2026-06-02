using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IHistorialAcademicoService
    {
        Task<IEnumerable<HistorialAcademico>> GetAllAsync();
        Task<HistorialAcademico?> GetByIdAsync(int id);
        Task<HistorialAcademico> CreateAsync(HistorialAcademico entity);
        Task UpdateAsync(HistorialAcademico entity, int id);
        Task UpdateStateAsync(int id, EstadoHistorialAcademico estado);
        Task DeleteAsync(int id);
        Task<List<HistorialAcademico>> GetByEstudianteIdAsync(int estudianteId);
        Task<IEnumerable<HistorialAcademico>> GetByDocumentAsync(string documento);
    }
}
