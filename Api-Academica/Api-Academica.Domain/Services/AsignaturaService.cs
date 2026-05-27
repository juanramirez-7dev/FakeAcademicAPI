using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class AsignaturaService : IAsignaturaService
    {
        private readonly IAsignaturaRepository _repository;

        public AsignaturaService(IAsignaturaRepository repository)
        {
            _repository = repository;

        }


        public async Task<IEnumerable<Asignatura>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Asignatura?> GetByIdAsync(int id)
        {
            Asignatura? asignatura = await _repository.GetByIdAsync(id);
            if (asignatura == null)
            {
                throw new KeyNotFoundException($"No se encontro una facultad con el id:{id}");
            }
            return asignatura;
        }
        public async Task<Asignatura> CreateAsync(Asignatura entity)
        {
            Asignatura? asignatura =
                await _repository.GetByCodigoAsync(
                    entity.PlanId,
                    entity.Codigo);

            if (asignatura != null)
            {
                throw new InvalidOperationException(
                    $"Ya existe una asignatura con el código: {entity.Codigo}");
            }

            return await _repository.CreateAsync(entity);
        }
        public async Task UpdateAsync(Asignatura entity, int id)
        {
            Asignatura? asignatura = await _repository.GetByIdAsync(id);

            if (asignatura == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró una asignatura con el id:{id}");
            }

            asignatura.Nombre = entity.Nombre;
            asignatura.Creditos = entity.Creditos;
            asignatura.SemestreRecomendado = entity.SemestreRecomendado;
            asignatura.Tipo = entity.Tipo;

            await _repository.UpdateAsync(asignatura);
        }


        public async Task DeleteAsync(int id)
        {
            Asignatura? asignatura = await _repository.GetByIdAsync(id);

            if (asignatura == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró una asignatura con el id:{id}");
            }

            await _repository.DeleteAsync(id);
        }

    }
}
