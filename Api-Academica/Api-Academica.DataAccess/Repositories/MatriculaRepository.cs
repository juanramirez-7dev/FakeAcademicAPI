using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Api_Academica.DataAccess.Repositories
{
    public class MatriculaRepository: IMatriculaRepository
    {
        private readonly AcademicDBContext _context;

        public MatriculaRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            return await _context.Matriculas.ToListAsync();
        }
        public async Task<Matricula?> GetByIdAsync(int id)
        {
            return await _context.Matriculas.FindAsync(id);
        }
        public async Task<Matricula> CreateAsync(Matricula entity)
        {
            await _context.Matriculas.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Matricula entity)
        {
            _context.Matriculas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Matricula>> GetByEstudianteIdAsync(int id)
        {
            return await _context.Matriculas.Where(e => e.EstudianteId == id).ToListAsync();
        }
        public async Task<Matricula?> GetByEstudiantePeriodoAsync(int estudianteId, int periodoId)
   
    
        {
            return await _context.Matriculas
                .FirstOrDefaultAsync(m =>
                    m.EstudianteId == estudianteId &&
                    m.PeriodoId == periodoId);
        }

    }
}
