using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.DataAccess.Repositories
{
    public class EstudianteRepository :IEstudianteRepository
    {
        private readonly AcademicDBContext _context;

        public EstudianteRepository(AcademicDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }
        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }
        public async Task<Estudiante?> GetByDocumentAsync(string document)
        {
            return await _context.Estudiantes
        .Include(e => e.Programa)
        .ThenInclude(p => p.Facultad)
        .FirstOrDefaultAsync(e => e.NumeroDocumento == document);
        }
        public async Task<Estudiante> CreateAsync(Estudiante entity)
        {
            await _context.Estudiantes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Estudiante entity)
        {
            _context.Estudiantes.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Estudiante? entity = await _context.Estudiantes.FindAsync(id);
            if (entity != null)
            {
                _context.Estudiantes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Estudiante>> GetByProgramaIdAsync(int id)
        {
            return await _context.Estudiantes.Where(e => e.ProgramaId == id).ToListAsync();
        }

        public async Task<Estudiante?> GetByCodigoEstudiantilAsync(string codigo)
        {
            return await _context.Estudiantes.FirstOrDefaultAsync(e => e.CodigoEstudiantil == codigo);
        }

        public async Task<Estudiante?> GetByCorreoInstitucionalAsync(string correo)
        {
            return await _context.Estudiantes.FirstOrDefaultAsync(e => e.CorreoInstitucional== correo);
        }

        public async Task<Estudiante?> GetByNumeroDocumentoAsync(string documento)
        {
            return await _context.Estudiantes.FirstOrDefaultAsync(e => e.NumeroDocumento == documento);
        }


    }



}
