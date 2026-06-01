using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Repositories
{
    public interface IHistorialAcademicoRepository
    {
        Task<IEnumerable<HistorialAcademico>> GetAllAsync();
        Task<HistorialAcademico?> GetByIdAsync(int id);
        Task<HistorialAcademico> CreateAsync(HistorialAcademico entity);
        Task UpdateAsync(HistorialAcademico entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<HistorialAcademico>> GetByEstudianteIdAsync(int estudianteId);
        Task<IEnumerable<HistorialAcademico>> GetByAsignaturaIdAsync(int asignaturaId);

        Task<HistorialAcademico?> GetByEstudianteAsignaturaPeriodoAsync(int estudianteId, int asignaturaId, int periodoId);

    }

}
