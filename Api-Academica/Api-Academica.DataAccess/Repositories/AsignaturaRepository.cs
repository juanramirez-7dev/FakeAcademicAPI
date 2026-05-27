using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Api_Academica.DataAccess.Repositories
{
    public class AsignaturaRepository:IAsignaturaRepository
    {
        private readonly AcademicDBContext _context;

        public AsignaturaRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asignatura>> GetAllAsync()
        {
            return await _context.Asignaturas.ToListAsync();
        }
        public async Task<Asignatura?> GetByIdAsync(int id)
        {
            return await _context.Asignaturas.FindAsync(id);
        }
        public async Task<Asignatura> CreateAsync(Asignatura entity)
        {
            await _context.Asignaturas.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Asignatura entity)
        {
            _context.Asignaturas.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Asignatura? entity = await _context.Asignaturas.FindAsync(id);
            if (entity != null)
            {
                _context.Asignaturas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Asignatura?> GetByCodigoAsync(int planId, string codigo)
          

        {
            return await _context.Asignaturas
                .FirstOrDefaultAsync(a =>
                    a.PlanId == planId &&
                    a.Codigo == codigo);
        }



    }
}
