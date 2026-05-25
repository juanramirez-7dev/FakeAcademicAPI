using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class EstudianteService:IEstudianteService
    {
        private readonly IEstudianteRepository _repository;

        public EstudianteService(IEstudianteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            var estudiante = await _repository.GetByIdAsync(id);
            if (estudiante == null)
            {
                throw new KeyNotFoundException($"El estudiante con el id {id} no se encontro");
            }
            return estudiante;
        }

        public async Task<Estudiante> CreateAsync(Estudiante entity)
        {
            
            return await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await _repository.GetByIdAsync(id);
            if (estudiante == null)
            {
                throw new KeyNotFoundException($"No se encontro un estudiante con el id: {id}");
            }
            if (estudiante.Estado == EstadoAcademicoEstudiante.Activo)
            {
                throw new InvalidOperationException($"No se puede borrar un estudiante activo");
            }
            await _repository.DeleteAsync(id);
        }
        public async Task UpdateAsync(Estudiante entity, int id)
        {
            var estudiante = await _repository.GetByIdAsync(id);
            if (estudiante == null)
            {
                throw new KeyNotFoundException($"No se encontro un estudiante con el id: {id}");
            }
            estudiante.CodigoEstudiantil = entity.CodigoEstudiantil;
            estudiante.TipoDocumento = entity.TipoDocumento;
            estudiante.NumeroDocumento = entity.NumeroDocumento;
            estudiante.Nombres=entity.Nombres;
            estudiante.Apellidos = entity.Apellidos;
            estudiante.CorreoInstitucional=entity.CorreoInstitucional;
            estudiante.Telefono=entity.Telefono;
            
         await _repository.UpdateAsync(estudiante);
        }

        public async Task UpdateStateAsync(int id, EstadoAcademicoEstudiante estado)
        {
            Estudiante? estudiante = await _repository.GetByIdAsync(id);
            if (estudiante == null)
            {
                throw new KeyNotFoundException($"No se encontro un estudiante con el id:{id}");
            }
            if (estudiante.Estado == estado)
            {
                throw new InvalidOperationException($"La facultad ya se encuentra con el estado {estado}");
            }
            estudiante.Estado = estado;
            await _repository.UpdateAsync(estudiante);
        }









    }
}
