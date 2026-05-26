using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class MatriculaService:IMatriculaService
    {
        
        private readonly IMatriculaRepository _repository;

        public MatriculaService (IMatriculaRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Matricula?> GetByIdAsync(int id)
        {
            var matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
            {
                throw new KeyNotFoundException($"La matricula con el id {id} no se encontro");
            }
            return matricula;
        }

        public async Task<Matricula> CreateAsync(Matricula entity)
        {

            return await _repository.CreateAsync(entity);
        }

        
        public async Task UpdateAsync(Matricula entity, int id) {
            Matricula? matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
            {
                throw new KeyNotFoundException($"No se encontro una matricula con el id:{id}");
            }
            await _repository.UpdateAsync(matricula);
        }

        public async Task UpdateStateAsync(int id, EstadoMatricula estado)
        {
            Matricula? matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
            {
                throw new KeyNotFoundException($"No se encontro una matricula con el id:{id}");
            }
            if (matricula.Estado == estado)
            {
                throw new InvalidOperationException($"La matricula ya se encuentra con el estado {estado}");
            }
            matricula.Estado = estado;
            await _repository.UpdateAsync(matricula);
        }

    }
}
