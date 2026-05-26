using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class PlanEstudio
    {
        public int PlanId { get; set; }
        public int ProgramaId { get; set; }
        public string Version { get; set; } = string.Empty;
        public EstadoPlanEstudio Estado { get; set; } = EstadoPlanEstudio.Activo;
        public Programa Programa { get; set; } = null!;
    }
}
