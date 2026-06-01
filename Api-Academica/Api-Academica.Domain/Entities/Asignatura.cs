using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class Asignatura
    {

        public int AsignaturaId { get; set; }
        public int PlanId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int SemestreRecomendado { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public PlanEstudio PlanEstudio { get; set; } = null!;
        public ICollection<HistorialAcademico> HistorialAcademicos { get; set; } = new List<HistorialAcademico>();

    }
}
