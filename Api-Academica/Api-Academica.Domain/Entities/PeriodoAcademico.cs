using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class PeriodoAcademico
    {
        public int PeriodoId { get; set; }
        public int Anio { get; set; }
        public int Semestre { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public EstadoPeriodoAcademico Estado { get; set; } = EstadoPeriodoAcademico.Abierto;



    }
}
