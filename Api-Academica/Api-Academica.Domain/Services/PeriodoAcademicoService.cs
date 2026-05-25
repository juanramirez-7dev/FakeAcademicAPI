using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{
    public class PeriodoAcademicoService:IPeriodoAcademicoService
    {
        private readonly IPeriodoAcademicoRepository _repository;


        public PeriodoAcademicoService(IPeriodoAcademicoRepository repository)
        {
            _repository=repository;
        }

        public async Task < IEnumerable < PeriodoAcademico >> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<PeriodoAcademico?> GetByIdAsync(int id)
        {
            PeriodoAcademico? periodoAcademico = await _repository.GetByIdAsync(id);
            if (periodoAcademico == null)
            {
                throw new KeyNotFoundException($"No se encontro un periodo acádemico con el id:{id}");
            }
            return periodoAcademico;
        }
        public async Task<PeriodoAcademico> CreateAsync(PeriodoAcademico entity)
        {
          
            return await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(PeriodoAcademico entity, int id)
        {
            PeriodoAcademico? periodoAcademico = await _repository.GetByIdAsync(id);
            if (periodoAcademico == null)
            {
                throw new KeyNotFoundException($"No se encontro un periodo acádemico con el id:{id}");
            }
            await _repository.UpdateAsync(periodoAcademico);
        }


        public async Task UpdateStateAsync(int id, EstadoPeriodoAcademico estado)
        {
            PeriodoAcademico? periodoAcademico = await _repository.GetByIdAsync(id);
            if (periodoAcademico == null)
            {
                throw new KeyNotFoundException($"No se encontro un periodo acádemico con  el id:{id}");
            }
            if (periodoAcademico.Estado == estado)
            {
                throw new InvalidOperationException($"El periodo acádemico ya se encuentra con el estado {estado}");
            }
            periodoAcademico.Estado = estado;
            await _repository.UpdateAsync(periodoAcademico);
        }
    }


}

