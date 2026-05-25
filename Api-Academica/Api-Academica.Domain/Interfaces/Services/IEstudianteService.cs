using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Interfaces.Services
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task<Estudiante> CreateAsync(Estudiante entity);
        Task UpdateAsync(Estudiante entity, int id);
        Task DeleteAsync(int id);
        Task UpdateStateAsync(int id, EstadoAcademicoEstudiante estado);
    }
}
