using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Services
{

    public class HistorialAcademicoService : IHistorialAcademicoService
    {
        private readonly IHistorialAcademicoRepository _repository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IAsignaturaRepository _asignaturaRepository;
        private readonly IPeriodoAcademicoRepository _periodoAcademicoRepository;
        public HistorialAcademicoService(IHistorialAcademicoRepository repository,
            IEstudianteRepository estudianteRepository, IAsignaturaRepository asignaturaRepository,
            IPeriodoAcademicoRepository periodoAcademicoRepository)

        {
            _repository = repository;
            _estudianteRepository = estudianteRepository;
            _asignaturaRepository = asignaturaRepository;
            _periodoAcademicoRepository = periodoAcademicoRepository;

        }

        public async Task<IEnumerable<HistorialAcademico>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<HistorialAcademico?> GetByIdAsync(int id)
        {
            var historial = await _repository.GetByIdAsync(id);

            if (historial == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un historial académico con el id: {id}");
            }

            return historial;
        }

        public async Task<HistorialAcademico> CreateAsync(HistorialAcademico entity)

        {
            var estudiante = await _estudianteRepository.GetByIdAsync(entity.EstudianteId);
            if (estudiante == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un estudiante con el id: {entity.EstudianteId}");
            }
            var asignatura = await _asignaturaRepository.GetByIdAsync(entity.AsignaturaId);

            if (asignatura == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró una asignatura con el id: {entity.AsignaturaId}");
            }
            var periodo = await _periodoAcademicoRepository.GetByIdAsync(entity.PeriodoId);
            if (periodo == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un período académico con el id: {entity.PeriodoId}");
            }
            if (entity.NotaFinal < 0 || entity.NotaFinal > 5)
            {
                throw new InvalidOperationException(
                    "La nota debe estar entre 0 y 5");

               
            }
            if (entity.CreditosAprobados < 0)
            {
                throw new InvalidOperationException(
                    "Los créditos aprobados no pueden ser negativos");
            }
            entity.Estado =
            entity.NotaFinal >= 3
            ? EstadoHistorialAcademico.Aprobada
            : EstadoHistorialAcademico.Reprobada;

            var historialExistente =
                 await _repository.GetByEstudianteAsignaturaPeriodoAsync(
                    entity.EstudianteId,
                    entity.AsignaturaId,
                    entity.PeriodoId);

            if (historialExistente != null)
            {
                throw new InvalidOperationException(
                    "Ya existe un historial académico para este estudiante, asignatura y período");
            }
            
            if (entity.Estado == EstadoHistorialAcademico.Aprobada &&
                entity.CreditosAprobados != asignatura.Creditos)
            {
                throw new InvalidOperationException(
                    "Los créditos aprobados deben coincidir con los créditos de la asignatura");
            }
            if (entity.Estado == EstadoHistorialAcademico.Reprobada &&
                entity.CreditosAprobados > 0)
            {
                throw new InvalidOperationException(
                    "Una asignatura reprobada no puede tener créditos aprobados");
            }

            return await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(HistorialAcademico entity, int id)
        {
            var historial = await _repository.GetByIdAsync(id);

            if (historial == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un historial académico con el id: {id}");
            }
            if (entity.NotaFinal < 0 || entity.NotaFinal > 5)
            {
                throw new InvalidOperationException(
                    "La nota debe estar entre 0 y 5");
            }
            if (historial.Estado == EstadoHistorialAcademico.Reprobada &&
                entity.CreditosAprobados > 0)
            {
                throw new InvalidOperationException(
                    "Una asignatura reprobada no puede tener créditos aprobados");
            }
            if (historial.Estado == EstadoHistorialAcademico.Aprobada &&
                entity.NotaFinal < 3)
            {
                throw new InvalidOperationException(
                   "Una asignatura aprobada no puede tener una nota inferior a 3");
            }

            if (historial.Estado == EstadoHistorialAcademico.Reprobada &&
            entity.NotaFinal >= 3)
            {
                throw new InvalidOperationException(
                    "Una asignatura reprobada no puede tener una nota aprobatoria");
            }

            var asignatura = await _asignaturaRepository.GetByIdAsync(historial.AsignaturaId);


            if (historial.Estado == EstadoHistorialAcademico.Aprobada &&
                entity.CreditosAprobados != asignatura!.Creditos)
            {
                throw new InvalidOperationException(
                    "Los créditos aprobados deben coincidir con los créditos de la asignatura");
            }


            historial.NotaFinal = entity.NotaFinal;
            historial.CreditosAprobados = entity.CreditosAprobados;

            await _repository.UpdateAsync(historial);
        }

        public async Task UpdateStateAsync(int id, EstadoHistorialAcademico estado)
        {
            HistorialAcademico? historialAcademico = await _repository.GetByIdAsync(id);

            if (historialAcademico == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un historial académico con el id: {id}");
            }

            if (historialAcademico.Estado == estado)
            {
                throw new InvalidOperationException(
                    $"El historial académico ya se encuentra con el estado {estado}");
            }

            if (historialAcademico.NotaFinal < 3 &&
                estado == EstadoHistorialAcademico.Aprobada)
            {
                throw new InvalidOperationException(
                    "No se puede aprobar una asignatura con nota inferior a 3");
            }

            if (historialAcademico.NotaFinal >= 3 &&
                estado == EstadoHistorialAcademico.Reprobada)
            {
                throw new InvalidOperationException(
                    "No se puede reprobar una asignatura con nota aprobatoria");
            }

            if (estado == EstadoHistorialAcademico.Reprobada &&
                historialAcademico.CreditosAprobados > 0)
            {
                throw new InvalidOperationException(
                    "Una asignatura reprobada no puede tener créditos aprobados");
            }

            historialAcademico.Estado = estado;

            await _repository.UpdateAsync(historialAcademico);
        }

        public async Task DeleteAsync(int id)
        {
            throw new InvalidOperationException(
                "No se permite eliminar historiales académicos");
        }

    }

    

}









