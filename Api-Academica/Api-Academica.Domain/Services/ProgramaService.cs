using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class ProgramaService:IProgramaService
    
       
    {
        private readonly IProgramaRepository _repository;

        public ProgramaService(IProgramaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Programa>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Programa?> GetByIdAsync(int id)
        {
            var programa= await _repository.GetByIdAsync(id);
            if(programa == null)
            {
                throw new KeyNotFoundException($"El programa con el id {id} no se encontro");
            }
            return programa;
        }

        public async Task<Programa> CreateAsync(Programa entity)
        {
            Programa? programa = await _repository.GetByNameAsync(entity.Nombre);
            if (programa != null)
            {
                throw new InvalidOperationException($"Ya existe un programa con el nombre: {entity.Nombre}");
            }
            return await _repository.CreateAsync(entity);
        }
        
        public async Task DeleteAsync(int id)
        {
            var programa = await _repository.GetByIdAsync(id);
            if (programa == null)
            {
                throw new KeyNotFoundException($"No se encontro un programa con el id: {id}");
            }
           
            await _repository.DeleteAsync(id);
        }
        public async Task UpdateAsync(Programa entity, int id)
        {
            var programa = await _repository.GetByIdAsync(id);
            if (programa == null)
            {
                throw new KeyNotFoundException($"No se encontro un programa con el id: {id}");
            }
            if (await _repository.GetByNameAsync(entity.Nombre) != null)
            {
                throw new InvalidOperationException($"Ya existe un programa con el nombre: {id}");
            }
            programa.Codigo= entity.Codigo;
            programa.Nombre= entity.Nombre;
            programa.Semestres= entity.Semestres;
            programa.CreditosTotales= entity.CreditosTotales;
            programa.Nivel= entity.Nivel;
            await _repository.UpdateAsync(programa);
        }
    }


}



