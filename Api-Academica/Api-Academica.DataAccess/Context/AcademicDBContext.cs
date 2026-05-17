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
        }
    }
}
