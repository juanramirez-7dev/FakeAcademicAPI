using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.DataAccess.Repositories
{
    public class ProgramaRepository: IProgramaRepository
    {

        private readonly AcademicDBContext _context;

        public ProgramaRepository(AcademicDBContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Programa>> GetAllAsync()
        {
            return await _context.Programas.ToListAsync();
        }
        public async Task<Programa?> GetByIdAsync(int id)
        {
            return await _context.Programas.FindAsync(id);
        }
        public async Task<Programa> CreateAsync(Programa entity)
        {
           await _context.Programas.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Programa entity)
        {
             _context.Programas.Update(entity);
           await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Programas.FindAsync(id);
            if (entity != null)
            {
                _context.Programas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Programa?> GetByNameAsync(string name)
        {
            return await _context.Programas.FirstOrDefaultAsync(e => e.Nombre == name);
        }

        public async Task<IEnumerable<Programa>> GetByFacultadIdAsync(int id)
        {
            return await _context.Programas.Where(e => e.FacultadId == id).ToListAsync();
        }


    }
}
