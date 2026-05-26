using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class Programa
    {
       

        public int ProgramaId { get; set; }
        public int FacultadId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nivel { get; set; }= string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int CreditosTotales { get; set; }
        public int Semestres { get; set; }
        public Facultad Facultad { get; set; } = null!;
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        public ICollection<PlanEstudio> PlanesEstudio { get; set; } = new List<PlanEstudio>();

    }
}
