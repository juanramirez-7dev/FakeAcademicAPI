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
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IHistorialAcademicoRepository _historialAcademicoRepository;
        private readonly IProgramaRepository _programaRepository;

        public EstudianteService(IEstudianteRepository repository,IMatriculaRepository matriculaRepository,
            IHistorialAcademicoRepository historialAcademicoRepository, IProgramaRepository programaRepository)
        {
            _repository = repository;
            _matriculaRepository = matriculaRepository;
            _historialAcademicoRepository = historialAcademicoRepository;
            _programaRepository = programaRepository;
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
            var programa = await _programaRepository.GetByIdAsync(entity.ProgramaId);

            if (programa == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un programa con el id: {entity.ProgramaId}");
            }

            if (string.IsNullOrWhiteSpace(entity.CodigoEstudiantil))
            {
                throw new InvalidOperationException(
                    "El código estudiantil es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.Nombres))
            {
                throw new InvalidOperationException(
                    "Los nombres son obligatorios");
            }

            if (string.IsNullOrWhiteSpace(entity.Apellidos))
            {
                throw new InvalidOperationException(
                    "Los apellidos son obligatorios");
            }

            if (string.IsNullOrWhiteSpace(entity.CorreoInstitucional))
            {
                throw new InvalidOperationException(
                    "El correo institucional es obligatorio");
            }

            if (await _repository.GetByCodigoEstudiantilAsync(entity.CodigoEstudiantil) != null)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el código {entity.CodigoEstudiantil}");
            }

            if (await _repository.GetByNumeroDocumentoAsync(entity.NumeroDocumento) != null)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el documento {entity.NumeroDocumento}");
            }

            if (await _repository.GetByCorreoInstitucionalAsync(entity.CorreoInstitucional) != null)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el correo {entity.CorreoInstitucional}");
            }
            entity.Estado = EstadoAcademicoEstudiante.Activo;
            return await _repository.CreateAsync(entity);
        }


        public async Task UpdateAsync(Estudiante entity, int id)
        {
            var estudiante = await _repository.GetByIdAsync(id);

            if (estudiante == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un estudiante con el id: {id}");
            }

            var estudianteCodigo =
                await _repository.GetByCodigoEstudiantilAsync(entity.CodigoEstudiantil);

            if (estudianteCodigo != null &&
                estudianteCodigo.EstudianteId != id)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el código {entity.CodigoEstudiantil}");
            }

            var estudianteDocumento =
                await _repository.GetByNumeroDocumentoAsync(entity.NumeroDocumento);

            if (estudianteDocumento != null &&
                estudianteDocumento.EstudianteId != id)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el documento {entity.NumeroDocumento}");
            }

            var estudianteCorreo =
                await _repository.GetByCorreoInstitucionalAsync(entity.CorreoInstitucional);

            if (estudianteCorreo != null &&
                estudianteCorreo.EstudianteId != id)
            {
                throw new InvalidOperationException(
                    $"Ya existe un estudiante con el correo {entity.CorreoInstitucional}");
            }

            estudiante.CodigoEstudiantil = entity.CodigoEstudiantil;
            estudiante.TipoDocumento = entity.TipoDocumento;
            estudiante.NumeroDocumento = entity.NumeroDocumento;
            estudiante.Nombres = entity.Nombres;
            estudiante.Apellidos = entity.Apellidos;
            estudiante.CorreoInstitucional = entity.CorreoInstitucional;
            estudiante.Telefono = entity.Telefono;

            await _repository.UpdateAsync(estudiante);
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
            var matriculas = await _matriculaRepository.GetByEstudianteIdAsync(estudiante.EstudianteId);

            if (matriculas.Any())
            {
                throw new InvalidOperationException($"No se puede borrar un estudiante con matriculas existentes");
            }
            var historialAcademicos  = await _historialAcademicoRepository.GetByEstudianteIdAsync(estudiante.EstudianteId);

            if (historialAcademicos.Any())
            {
                throw new InvalidOperationException($"No se puede borrar un estudiante con historial académico");
            }
            await _repository.DeleteAsync(id);
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
                throw new InvalidOperationException($"El estudiante ya se encuentra con el estado {estado}");
            }
            estudiante.Estado = estado;
            await _repository.UpdateAsync(estudiante);
        }









    }
}
