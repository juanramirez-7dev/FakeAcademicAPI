using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class HistorialAcademico

    {
        public int HistorialId { get; set; }

        public int EstudianteId { get; set; }
        public int AsignaturaId { get; set; }
        public int PeriodoId { get; set; }

        public decimal NotaFinal { get; set; }

        public EstadoHistorialAcademico Estado { get; set; }

        public int CreditosAprobados { get; set; }

        public Estudiante Estudiante { get; set; } = null!;
        public Asignatura Asignatura { get; set; } = null!;
        public PeriodoAcademico PeriodoAcademico { get; set; } = null!;
    }
}
