using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class Matricula

    {
        public int MatriculaId { get; set; }
        public int EstudianteId { get; set; }
        public int PeriodoId { get; set; }
        public DateOnly FechaMatricula { get; set; }
        public EstadoMatricula Estado { get; set; } = EstadoMatricula.Activa;
        public PeriodoAcademico Periodo { get; set; } = null!;
        public Estudiante Estudiante { get; set; } = null!;

    }
}
