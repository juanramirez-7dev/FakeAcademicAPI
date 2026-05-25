using Api_Academica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.DataAccess.Context
{
    public class AcademicDBContext : DbContext
    {
        public AcademicDBContext(DbContextOptions<AcademicDBContext> options) : base(options)
        {
        }

        public DbSet<Facultad> Facultades { get; set; }

        public DbSet<Programa> Programas { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Facultad>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.Estado)
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Programa>(entity =>
            {
                entity.HasKey(e => e.ProgramaId);
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Nombre)
                   .IsRequired()
                   .HasMaxLength(150);
                entity.Property(e => e.Nivel)
                   .IsRequired()
                   .HasMaxLength(30);
                entity.Property(e => e.CreditosTotales)
                   .IsRequired();
                entity.Property(e => e.Semestres)
                 .IsRequired();
                entity.HasOne(p => p.Facultad).
                WithMany(f => f.Programas).
                HasForeignKey(p => p.FacultadId).
                OnDelete(DeleteBehavior.Restrict);



            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.EstudianteId);
                entity.Property(e => e.CodigoEstudiantil)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(e => e.TipoDocumento)
                   .IsRequired()
                   .HasMaxLength(10);
                entity.Property(e => e.NumeroDocumento)
                   .IsRequired()
                   .HasMaxLength(30);
                entity.Property(e => e.Nombres)
                   .IsRequired()
                   .HasMaxLength(100);
                entity.Property(e => e.Apellidos)
                   .IsRequired()
                   .HasMaxLength(100);
                entity.Property(e => e.CorreoInstitucional)
                   .IsRequired()
                   .HasMaxLength(120);
                entity.Property(e => e.Telefono)
                   .IsRequired()
                   .HasMaxLength(30);
                entity.Property(e => e.FechaIngreso)
                   .IsRequired()
                   .HasColumnType("date");
                entity.HasOne(e => e.Programa)
                   .WithMany(p => p.Estudiantes)
                   .HasForeignKey(e => e.ProgramaId)
                   .OnDelete(DeleteBehavior.Restrict);
                    



            });

        }
        

        
    }
}
