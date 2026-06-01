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
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IPeriodoAcademicoRepository _periodoAcademicoRepository;

        public MatriculaService (IMatriculaRepository repository, IEstudianteRepository estudianteRepository,
            IPeriodoAcademicoRepository periodoAcademicoRepository)
        {
            _repository = repository;
            _estudianteRepository = estudianteRepository;
            _periodoAcademicoRepository = periodoAcademicoRepository;
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
            var estudiante = await _estudianteRepository.GetByIdAsync(entity.EstudianteId);

            if (estudiante == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un estudiante con el id: {entity.EstudianteId}");
            }
            var periodo = await _periodoAcademicoRepository.GetByIdAsync(entity.PeriodoId);

            if (periodo == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un período académico con el id: {entity.PeriodoId}");
            }

            if (periodo.Estado != EstadoPeriodoAcademico.Abierto)
            {
                throw new InvalidOperationException(
                    "No se puede matricular un estudiante en un período cerrado");
            }

            var matriculaExistente =
                await _repository.GetByEstudiantePeriodoAsync(
                    entity.EstudianteId,
                    entity.PeriodoId);

            if (matriculaExistente != null)
            {
                throw new InvalidOperationException(
                    "El estudiante ya se encuentra matriculado en este período académico");
            }

            if (entity.FechaMatricula > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new InvalidOperationException(
                    "La fecha de matrícula no puede ser futura");
            }

            entity.Estado = EstadoMatricula.Activa;

            return await _repository.CreateAsync(entity);
        }

           

        
        public async Task UpdateAsync(Matricula entity, int id) 
        {
            Matricula? matricula = await _repository.GetByIdAsync(id);
            if (matricula == null)
            {
                throw new KeyNotFoundException($"No se encontro una matricula con el id:{id}");
            }
            throw new InvalidOperationException(
                "No se permite modificar una matrícula. Solo se puede actualizar su estado.");
            
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
