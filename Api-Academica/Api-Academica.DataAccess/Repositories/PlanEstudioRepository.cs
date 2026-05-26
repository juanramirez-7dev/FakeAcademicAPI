using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Api_Academica.DataAccess.Repositories
{
    public class PlanEstudioRepository:IPlanEstudioRepository
    {

        private readonly AcademicDBContext _context;

        public PlanEstudioRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanEstudio>> GetAllAsync()
        {
            return await _context.PlanesEstudio.ToListAsync();
        }
        public async Task<PlanEstudio?> GetByIdAsync(int id)
        {
            return await _context.PlanesEstudio.FindAsync(id);
        }
        public async Task<PlanEstudio> CreateAsync(PlanEstudio entity)
        {
            await _context.PlanesEstudio.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(PlanEstudio entity)
        {
            _context.PlanesEstudio.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<PlanEstudio>> GetByProgramaIdAsync(int id)
        {
            return await _context.PlanesEstudio.Where(e => e.ProgramaId == id).ToListAsync();
        }


    }
}


