using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Api_Academica.Domain.Interfaces.Repositories;

namespace Api_Academica.DataAccess.Repositories
{
    public class HistorialAcademicoRepository:IHistorialAcademicoRepository
    {
        private readonly AcademicDBContext _context;

        public HistorialAcademicoRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistorialAcademico>> GetAllAsync()
        {
            return await _context.HistorialAcademicos.ToListAsync();
        }

        public async Task<HistorialAcademico?> GetByIdAsync(int id)
        {
            return await _context.HistorialAcademicos.FindAsync(id);
        }

        public async Task<HistorialAcademico> CreateAsync(HistorialAcademico entity)
        {
            await _context.HistorialAcademicos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(HistorialAcademico entity)
        {
            _context.HistorialAcademicos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            HistorialAcademico? entity =
                await _context.HistorialAcademicos.FindAsync(id);

            if (entity != null)
            {
                _context.HistorialAcademicos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<HistorialAcademico>> GetByEstudianteIdAsync(int estudianteId)
        {
            return await _context.HistorialAcademicos
                .Where(h => h.EstudianteId == estudianteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistorialAcademico>> GetByDocumentAsync(string documento)
        {
            return await _context.HistorialAcademicos
                .Where(h => h.Estudiante.NumeroDocumento == documento)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistorialAcademico>> GetByAsignaturaIdAsync(int asignaturaId)
        {
            return await _context.HistorialAcademicos
                .Where(h => h.AsignaturaId == asignaturaId)
                .ToListAsync();
        }

        public async Task<HistorialAcademico?> GetByEstudianteAsignaturaPeriodoAsync(int estudianteId, int asignaturaId, int periodoId)

        {
            return await _context.HistorialAcademicos
                .FirstOrDefaultAsync(h =>
                    h.EstudianteId == estudianteId &&
                    h.AsignaturaId == asignaturaId &&
                    h.PeriodoId == periodoId);
        }


    }

}
