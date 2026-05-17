using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class FacultadService:IFacultadService
    {
        private readonly IFacultadRepository _repository;
        public FacultadService(IFacultadRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Facultad>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Facultad?> GetByIdAsync(int id)
        {
            Facultad? facultad = await _repository.GetByIdAsync(id);
            if (facultad == null)
            {
                throw new KeyNotFoundException($"No se encontro una facultad con el id:{id}");
            }
            return facultad;
        }
        public async Task<Facultad> CreateAsync(Facultad entity)
        {
            Facultad? facultad = await _repository.GetByNameAsync(entity.Nombre);
            if (facultad != null)
            {
                throw new InvalidOperationException($"Ya existe una facultad con el nombre: {entity.Nombre}");
            }
            return await _repository.CreateAsync(entity);
        }
        public async Task UpdateAsync(Facultad entity, int id)
        {
            Facultad? facultad = await _repository.GetByIdAsync(id);
            if (facultad == null)
            {
                throw new KeyNotFoundException($"No se encontro una facultad con el id:{id}");
            }
            if (await _repository.GetByNameAsync(entity.Nombre) != null)
            {
                throw new InvalidOperationException($"Ya existe una facultad con el nombre: {entity.Nombre}");
            }
            facultad.Codigo = entity.Codigo;
            facultad.Nombre = entity.Nombre;
            await _repository.UpdateAsync(facultad);
        }
        public async Task DeleteAsync(int id)
        {
            Facultad? facultad = await _repository.GetByIdAsync(id);
            if (facultad == null)
            {
                throw new KeyNotFoundException($"No se encontro una facultad con el id:{id}");
            }
            if (facultad.Estado == EstadoFacultad.Activo)
            {
                throw new InvalidOperationException($"No se puede borrar una facultad activa");
            }
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateStateAsync(int id,EstadoFacultad estado)
        {
            Facultad? facultad = await _repository.GetByIdAsync(id);
            if (facultad == null)
            {
                throw new KeyNotFoundException($"No se encontro una facultad con el id:{id}");
            }
            if (facultad.Estado == estado)
            {
                throw new InvalidOperationException($"La facultad ya se encuentra con el estado {estado}");
            }
            facultad.Estado = estado;
            await _repository.UpdateAsync(facultad);
        }
    }
}
