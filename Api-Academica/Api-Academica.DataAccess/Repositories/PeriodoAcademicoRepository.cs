using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.DataAccess.Repositories
{
    public class PeriodoAcademicoRepository:IPeriodoAcademicoRepository
    {
        private readonly AcademicDBContext _context;

        public PeriodoAcademicoRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PeriodoAcademico>> GetAllAsync()
        {
            return await _context.PeriodosAcademicos.ToListAsync();
        }
        public async Task<PeriodoAcademico?> GetByIdAsync(int id)
        {
            return await _context.PeriodosAcademicos.FindAsync(id);
        }
        public async Task<PeriodoAcademico> CreateAsync(PeriodoAcademico entity)
        {
            await _context.PeriodosAcademicos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(PeriodoAcademico entity)
        {
            _context.PeriodosAcademicos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PeriodoAcademico?> GetByAnioSemestreAsync(int anio, int semestre)

        {
            return await _context.PeriodosAcademicos
                .FirstOrDefaultAsync(p =>
                    p.Anio == anio &&
                    p.Semestre == semestre);
        }

    }
}
