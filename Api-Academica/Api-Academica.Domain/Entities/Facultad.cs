using Api_Academica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Academica.Domain.Entities
{
    public class Facultad
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public EstadoFacultad Estado { get; set; } = EstadoFacultad.Activo;
        public ICollection<Programa> Programas { get; set; }=new List<Programa>();
    }
}
