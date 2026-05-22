using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.DataAccess.Repositories
{
    public class FacultadRepository : IFacultadRepository
    {
        private readonly AcademicDBContext _context;

        public FacultadRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Facultad>> GetAllAsync()
        {
            return await _context.Facultades.ToListAsync();
        }
        public async Task<Facultad?> GetByIdAsync(int id)
        {
            return await _context.Facultades.FindAsync(id);
        }
        public async Task<Facultad> CreateAsync(Facultad entity)
        {
            await _context.Facultades.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Facultad entity)
        {
            _context.Facultades.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Facultad? entity = await _context.Facultades.FindAsync(id);
            if (entity != null)
            {
                _context.Facultades.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Facultades.AnyAsync(e => e.Id == id);
        }

        public async Task<Facultad?> GetByNameAsync(string name)
        {
            return await _context.Facultades.FirstOrDefaultAsync(e => e.Nombre == name);
        }

      
    }
}
