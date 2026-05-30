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

        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }

        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<PlanEstudio> PlanesEstudio { get; set; }

        public DbSet<Asignatura> Asignaturas { get; set; }

        public DbSet<HistorialAcademico> HistorialAcademicos { get; set; }


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
                entity.Property(e => e.Estado)
                    .HasMaxLength(20);
                entity.HasOne(e => e.Programa)
                   .WithMany(p => p.Estudiantes)
                   .HasForeignKey(e => e.ProgramaId)
                   .OnDelete(DeleteBehavior.Restrict);
                    



            });

            modelBuilder.Entity<PeriodoAcademico>(entity =>
            {
                entity.HasKey(e => e.PeriodoId);
                entity.Property(e => e.Anio)
                   .IsRequired();
                entity.Property(e => e.Semestre)
                   .IsRequired();
                entity.Property(e => e.FechaInicio)
                   .IsRequired()
                   .HasColumnType("date");
                entity.Property(e => e.FechaFin)
                   .IsRequired()
                   .HasColumnType("date");
                entity.Property(e => e.Estado)
                    .HasMaxLength(20);
            });



            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => e.MatriculaId);
                entity.HasIndex(e => new
                {
                    e.EstudianteId,
                    e.PeriodoId
                }).IsUnique();
                entity.Property(e => e.FechaMatricula)
                   .IsRequired()
                   .HasColumnType("date");
                entity.Property(e => e.Estado)
                    .HasMaxLength(20);
                entity.HasOne(e => e.Estudiante)
                   .WithMany(p => p.Matriculas)
                   .HasForeignKey(e => e.EstudianteId)
                   .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Periodo)
                   .WithMany(p => p.Matriculas)
                   .HasForeignKey(e => e.PeriodoId)
                   .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<PlanEstudio>(entity =>
            {
                entity.HasKey(e => e.PlanId);
                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Estado)
                    .HasMaxLength(20);
                entity.HasIndex(e => new
                {
                    e.ProgramaId,
                    e.Version
                }).IsUnique();
                entity.HasOne(e => e.Programa)
        .WithMany(p => p.PlanesEstudio)
        .HasForeignKey(e => e.ProgramaId)
        .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasKey(e => e.AsignaturaId);
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.Creditos)
                    .IsRequired();
                entity.Property(e => e.SemestreRecomendado)
                    .IsRequired();
                entity.HasIndex(e => new
                {
                    e.PlanId,
                    e.Codigo
                }).IsUnique();
                entity.HasOne(e => e.PlanEstudio)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(e => e.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<HistorialAcademico>(entity =>
            {
                entity.HasKey(e => e.HistorialId);

                entity.Property(e => e.NotaFinal)
                    .HasColumnType("decimal(3,2)")
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .IsRequired();

                entity.Property(e => e.CreditosAprobados)
                    .IsRequired();

                entity.HasOne(e => e.Estudiante)
                    .WithMany(e => e.HistorialAcademicos)
                    .HasForeignKey(e => e.EstudianteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Asignatura)
                    .WithMany(a => a.HistorialAcademicos)
                    .HasForeignKey(e => e.AsignaturaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PeriodoAcademico)
                    .WithMany(p => p.HistorialAcademicos)
                    .HasForeignKey(e => e.PeriodoId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new
                {
                    e.EstudianteId,
                    e.AsignaturaId,
                    e.PeriodoId
                }).IsUnique();
            });








        }
        

        
    }
}
