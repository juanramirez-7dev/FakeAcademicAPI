using System;
using System.Collections.Generic;
using System.Linq;
using Api_Academica.DataAccess.Context;
using Api_Academica.Domain.Entities;
using Api_Academica.Domain.Enums;

namespace Api_Academica.DataAccess.Seeders
{
    public static class AcademicDataSeeder
    {
        private const int StudentCount = 100;

        public static void Seed(AcademicDBContext context)
        {
            if (context.Facultades.Any())
            {
                return;
            }

            var random = new Random(20260531);

            var facultad = new Facultad
            {
                Codigo = "FI-ING",
                Nombre = "Facultad de Ingenierias",
                Estado = EstadoFacultad.Activo
            };

            var programas = BuildProgramas(facultad);
            var planes = BuildPlanes(programas);
            var asignaturas = BuildAsignaturas(planes);
            var periodos = BuildPeriodos();

            context.Facultades.Add(facultad);
            context.Programas.AddRange(programas);
            context.PlanesEstudio.AddRange(planes);
            context.Asignaturas.AddRange(asignaturas);
            context.PeriodosAcademicos.AddRange(periodos);
            context.SaveChanges();

            var studentPlans = BuildStudentPlans(programas, periodos, random);
            context.Estudiantes.AddRange(studentPlans.Select(p => p.Estudiante));
            context.SaveChanges();

            var matriculas = new List<Matricula>();
            var historiales = new List<HistorialAcademico>();

            foreach (var plan in studentPlans)
            {
                var planEstudio = planes.First(p => p.ProgramaId == plan.Estudiante.ProgramaId);
                var planAsignaturas = asignaturas.Where(a => a.PlanId == planEstudio.PlanId).ToList();
                BuildMatriculasYHistorial(plan, periodos, planAsignaturas, random, matriculas, historiales);
            }

            context.Matriculas.AddRange(matriculas);
            context.HistorialAcademicos.AddRange(historiales);
            context.SaveChanges();
        }

        private static List<Programa> BuildProgramas(Facultad facultad)
        {
            return new List<Programa>
            {
                new Programa
                {
                    Codigo = "T-ELEC",
                    Nombre = "Tecnologia en Electronica",
                    Nivel = "Tecnologia",
                    Semestres = 6,
                    CreditosTotales = 96,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "T-TEL",
                    Nombre = "Tecnologia en Telecomunicaciones",
                    Nivel = "Tecnologia",
                    Semestres = 6,
                    CreditosTotales = 96,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "T-EMEC",
                    Nombre = "Tecnologia en Electromecanica",
                    Nivel = "Tecnologia",
                    Semestres = 6,
                    CreditosTotales = 96,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "T-BIOM",
                    Nombre = "Tecnologia en Mantenimiento Biomedico",
                    Nivel = "Tecnologia",
                    Semestres = 6,
                    CreditosTotales = 96,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "T-PROD",
                    Nombre = "Tecnologia en Produccion",
                    Nivel = "Tecnologia",
                    Semestres = 6,
                    CreditosTotales = 96,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "I-ELEC",
                    Nombre = "Ingenieria Electronica",
                    Nivel = "Ingenieria",
                    Semestres = 10,
                    CreditosTotales = 160,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "I-TEL",
                    Nombre = "Ingenieria de Telecomunicaciones",
                    Nivel = "Ingenieria",
                    Semestres = 10,
                    CreditosTotales = 160,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "I-EMEC",
                    Nombre = "Ingenieria Electromecanica",
                    Nivel = "Ingenieria",
                    Semestres = 10,
                    CreditosTotales = 160,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "I-MECAT",
                    Nombre = "Ingenieria Mecatronica",
                    Nivel = "Ingenieria",
                    Semestres = 10,
                    CreditosTotales = 160,
                    Facultad = facultad
                },
                new Programa
                {
                    Codigo = "I-PROD",
                    Nombre = "Ingenieria de Produccion",
                    Nivel = "Ingenieria",
                    Semestres = 10,
                    CreditosTotales = 160,
                    Facultad = facultad
                }
            };
        }

        private static List<PlanEstudio> BuildPlanes(IEnumerable<Programa> programas)
        {
            return programas
                .Select(programa => new PlanEstudio
                {
                    Programa = programa,
                    Version = "2024-1",
                    Estado = EstadoPlanEstudio.Activo
                })
                .ToList();
        }

        private static List<Asignatura> BuildAsignaturas(IEnumerable<PlanEstudio> planes)
        {
            var asignaturas = new List<Asignatura>();

            foreach (var plan in planes)
            {
                var seeds = BuildBaseAsignaturas();
                seeds.AddRange(BuildSpecificAsignaturas(plan.Programa.Codigo));

                var prefix = plan.Programa.Codigo.Replace("-", string.Empty).ToUpperInvariant();
                var index = 1;

                foreach (var seed in seeds)
                {
                    asignaturas.Add(new Asignatura
                    {
                        PlanEstudio = plan,
                        Codigo = $"{prefix}-{index:D2}",
                        Nombre = seed.Nombre,
                        Creditos = seed.Creditos,
                        SemestreRecomendado = seed.Semestre,
                        Tipo = seed.Tipo
                    });
                    index++;
                }
            }

            return asignaturas;
        }

        private static List<PeriodoAcademico> BuildPeriodos()
        {
            return new List<PeriodoAcademico>
            {
                new PeriodoAcademico
                {
                    Anio = 2023,
                    Semestre = 1,
                    FechaInicio = new DateOnly(2023, 1, 16),
                    FechaFin = new DateOnly(2023, 6, 10),
                    Estado = EstadoPeriodoAcademico.Cerrado
                },
                new PeriodoAcademico
                {
                    Anio = 2023,
                    Semestre = 2,
                    FechaInicio = new DateOnly(2023, 7, 10),
                    FechaFin = new DateOnly(2023, 11, 25),
                    Estado = EstadoPeriodoAcademico.Cerrado
                },
                new PeriodoAcademico
                {
                    Anio = 2024,
                    Semestre = 1,
                    FechaInicio = new DateOnly(2024, 1, 22),
                    FechaFin = new DateOnly(2024, 6, 15),
                    Estado = EstadoPeriodoAcademico.Cerrado
                },
                new PeriodoAcademico
                {
                    Anio = 2024,
                    Semestre = 2,
                    FechaInicio = new DateOnly(2024, 7, 15),
                    FechaFin = new DateOnly(2024, 11, 30),
                    Estado = EstadoPeriodoAcademico.Cerrado
                },
                new PeriodoAcademico
                {
                    Anio = 2025,
                    Semestre = 1,
                    FechaInicio = new DateOnly(2025, 1, 20),
                    FechaFin = new DateOnly(2025, 6, 14),
                    Estado = EstadoPeriodoAcademico.Cerrado
                },
                new PeriodoAcademico
                {
                    Anio = 2025,
                    Semestre = 2,
                    FechaInicio = new DateOnly(2025, 7, 14),
                    FechaFin = new DateOnly(2025, 11, 29),
                    Estado = EstadoPeriodoAcademico.Abierto
                }
            };
        }

        private static List<StudentSeedPlan> BuildStudentPlans(
            IReadOnlyList<Programa> programas,
            IReadOnlyList<PeriodoAcademico> periodos,
            Random random)
        {
            var firstNames = new[]
            {
                "Ana", "Luis", "Carlos", "Maria", "Jorge", "Diana", "Juan", "Sofia",
                "Daniel", "Laura", "Andres", "Camila", "Miguel", "Paula", "Julian",
                "Valentina", "Santiago", "Andrea", "Felipe", "Natalia"
            };
            var lastNames = new[]
            {
                "Gomez", "Perez", "Rodriguez", "Garcia", "Martinez", "Lopez", "Torres",
                "Ramirez", "Sanchez", "Castro", "Vargas", "Moreno", "Herrera", "Ortiz"
            };

            var plans = new List<StudentSeedPlan>();

            for (var i = 1; i <= StudentCount; i++)
            {
                var programa = programas[random.Next(programas.Count)];
                var semestreCount = random.Next(2, 7);
                var startIndex = random.Next(0, periodos.Count - semestreCount + 1);
                var fechaIngreso = periodos[startIndex].FechaInicio;

                var estudiante = new Estudiante
                {
                    ProgramaId = programa.ProgramaId,
                    CodigoEstudiantil = $"2024{i:D4}",
                    TipoDocumento = "CC",
                    NumeroDocumento = $"10{i:D7}",
                    Nombres = firstNames[random.Next(firstNames.Length)],
                    Apellidos = lastNames[random.Next(lastNames.Length)],
                    CorreoInstitucional = $"estudiante{i:D3}@itm.edu.co",
                    Telefono = $"300{(1000000 + i)}",
                    FechaIngreso = fechaIngreso,
                    Estado = random.NextDouble() < 0.1
                        ? EstadoAcademicoEstudiante.Suspendido
                        : EstadoAcademicoEstudiante.Activo
                };

                plans.Add(new StudentSeedPlan(estudiante, startIndex, semestreCount));
            }

            return plans;
        }

        private static void BuildMatriculasYHistorial(
            StudentSeedPlan plan,
            IReadOnlyList<PeriodoAcademico> periodos,
            IReadOnlyList<Asignatura> asignaturas,
            Random random,
            List<Matricula> matriculas,
            List<HistorialAcademico> historiales)
        {
            var aprobadas = new HashSet<int>();
            var reprobadas = new HashSet<int>();

            for (var termIndex = 0; termIndex < plan.Semestres; termIndex++)
            {
                var periodo = periodos[plan.StartPeriodIndex + termIndex];

                matriculas.Add(new Matricula
                {
                    EstudianteId = plan.Estudiante.EstudianteId,
                    PeriodoId = periodo.PeriodoId,
                    FechaMatricula = periodo.FechaInicio.AddDays(7),
                    Estado = EstadoMatricula.Activa
                });

                var semestre = termIndex + 1;
                var elegibles = asignaturas
                    .Where(a => a.SemestreRecomendado <= semestre)
                    .ToList();

                var seleccionadas = new List<Asignatura>();
                var retakes = elegibles
                    .Where(a => reprobadas.Contains(a.AsignaturaId))
                    .OrderBy(_ => random.Next())
                    .Take(random.Next(0, 3))
                    .ToList();

                seleccionadas.AddRange(retakes);

                var restantes = elegibles
                    .Where(a => !aprobadas.Contains(a.AsignaturaId))
                    .Where(a => seleccionadas.All(s => s.AsignaturaId != a.AsignaturaId))
                    .OrderBy(_ => random.Next())
                    .ToList();

                var cantidad = random.Next(4, 7);

                foreach (var asignatura in restantes)
                {
                    if (seleccionadas.Count >= cantidad)
                    {
                        break;
                    }

                    seleccionadas.Add(asignatura);
                }

                foreach (var asignatura in seleccionadas)
                {
                    var nota = GenerarNota(random);
                    var estado = nota >= 3.0m
                        ? EstadoHistorialAcademico.Aprobada
                        : EstadoHistorialAcademico.Reprobada;

                    historiales.Add(new HistorialAcademico
                    {
                        EstudianteId = plan.Estudiante.EstudianteId,
                        AsignaturaId = asignatura.AsignaturaId,
                        PeriodoId = periodo.PeriodoId,
                        NotaFinal = nota,
                        Estado = estado,
                        CreditosAprobados = estado == EstadoHistorialAcademico.Aprobada
                            ? asignatura.Creditos
                            : 0
                    });

                    if (estado == EstadoHistorialAcademico.Aprobada)
                    {
                        aprobadas.Add(asignatura.AsignaturaId);
                        reprobadas.Remove(asignatura.AsignaturaId);
                    }
                    else
                    {
                        reprobadas.Add(asignatura.AsignaturaId);
                    }
                }
            }
        }

        private static decimal GenerarNota(Random random)
        {
            var pierde = random.NextDouble() < 0.25;
            var valor = pierde
                ? 1.5 + (random.NextDouble() * 1.4)
                : 3.0 + (random.NextDouble() * 2.0);

            return Math.Round((decimal)valor, 2, MidpointRounding.AwayFromZero);
        }

        private static List<AsignaturaSeed> BuildBaseAsignaturas()
        {
            return new List<AsignaturaSeed>
            {
                new AsignaturaSeed("Matematicas Basicas", 1, 3, "Basica"),
                new AsignaturaSeed("Calculo Diferencial", 1, 4, "Basica"),
                new AsignaturaSeed("Fisica I", 1, 3, "Basica"),
                new AsignaturaSeed("Quimica General", 1, 3, "Basica"),
                new AsignaturaSeed("Programacion I", 1, 3, "Basica"),
                new AsignaturaSeed("Ingles I", 1, 2, "Humanidades"),
                new AsignaturaSeed("Algebra Lineal", 2, 3, "Basica"),
                new AsignaturaSeed("Calculo Integral", 2, 4, "Basica"),
                new AsignaturaSeed("Fisica II", 2, 3, "Basica"),
                new AsignaturaSeed("Programacion II", 2, 3, "Basica"),
                new AsignaturaSeed("Ingles II", 2, 2, "Humanidades"),
                new AsignaturaSeed("Estadistica", 3, 3, "Basica")
            };
        }

        private static List<AsignaturaSeed> BuildSpecificAsignaturas(string programaCodigo)
        {
            return programaCodigo switch
            {
                "T-ELEC" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Circuitos I", 2, 3, "Profesional"),
                    new AsignaturaSeed("Circuitos II", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Analogica", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Digital", 3, 3, "Profesional"),
                    new AsignaturaSeed("Instrumentacion Industrial", 4, 3, "Profesional"),
                    new AsignaturaSeed("Control Automatico", 4, 3, "Profesional"),
                    new AsignaturaSeed("Microcontroladores", 4, 3, "Profesional"),
                    new AsignaturaSeed("Sensores y Actuadores", 4, 3, "Profesional"),
                    new AsignaturaSeed("PLC y Automatizacion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Electronica de Potencia", 5, 3, "Profesional"),
                    new AsignaturaSeed("Mantenimiento Electronico", 5, 2, "Profesional"),
                    new AsignaturaSeed("Proyecto Integrador I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Integrador II", 6, 3, "Profesional")
                },
                "T-TEL" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Fundamentos de Telecomunicaciones", 3, 3, "Profesional"),
                    new AsignaturaSeed("Redes de Datos I", 3, 3, "Profesional"),
                    new AsignaturaSeed("Redes de Datos II", 4, 3, "Profesional"),
                    new AsignaturaSeed("Comunicaciones Analogas", 3, 3, "Profesional"),
                    new AsignaturaSeed("Comunicaciones Digitales", 4, 3, "Profesional"),
                    new AsignaturaSeed("Radiocomunicaciones", 4, 3, "Profesional"),
                    new AsignaturaSeed("Antenas y Propagacion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Conmutacion y Senalizacion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Telefonia IP", 5, 2, "Profesional"),
                    new AsignaturaSeed("Cableado Estructurado", 3, 2, "Profesional"),
                    new AsignaturaSeed("Seguridad en Redes", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Telecom I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Telecom II", 6, 3, "Profesional")
                },
                "T-EMEC" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Mecanica Aplicada", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electrotecnia", 3, 3, "Profesional"),
                    new AsignaturaSeed("Maquinas Electricas I", 4, 3, "Profesional"),
                    new AsignaturaSeed("Maquinas Electricas II", 5, 3, "Profesional"),
                    new AsignaturaSeed("Hidraulica y Neumatica", 4, 3, "Profesional"),
                    new AsignaturaSeed("Metrologia Industrial", 4, 2, "Profesional"),
                    new AsignaturaSeed("Mantenimiento Electromecanico", 5, 3, "Profesional"),
                    new AsignaturaSeed("Sistemas de Potencia", 5, 3, "Profesional"),
                    new AsignaturaSeed("Automatizacion Industrial", 5, 3, "Profesional"),
                    new AsignaturaSeed("Taller de Mantenimiento", 4, 2, "Profesional"),
                    new AsignaturaSeed("Proyecto Electromecanico I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Electromecanico II", 6, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Mantenimiento", 6, 2, "Profesional")
                },
                "T-BIOM" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Fundamentos de Biomedicina", 3, 3, "Profesional"),
                    new AsignaturaSeed("Anatomia y Fisiologia", 2, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Medica", 3, 3, "Profesional"),
                    new AsignaturaSeed("Instrumentacion Biomedica", 4, 3, "Profesional"),
                    new AsignaturaSeed("Metrologia Biomedica", 4, 3, "Profesional"),
                    new AsignaturaSeed("Equipos Medicos I", 4, 3, "Profesional"),
                    new AsignaturaSeed("Equipos Medicos II", 5, 3, "Profesional"),
                    new AsignaturaSeed("Seguridad Electrica en Salud", 4, 2, "Profesional"),
                    new AsignaturaSeed("Gestion Hospitalaria", 5, 2, "Profesional"),
                    new AsignaturaSeed("Calidad y Normativa en Salud", 5, 2, "Profesional"),
                    new AsignaturaSeed("Mantenimiento Hospitalario", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Biomedico I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Biomedico II", 6, 3, "Profesional")
                },
                "T-PROD" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Procesos de Manufactura", 3, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Produccion", 4, 3, "Profesional"),
                    new AsignaturaSeed("Planeacion y Control de Produccion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Metodos y Tiempos", 3, 3, "Profesional"),
                    new AsignaturaSeed("Calidad y Mejora Continua", 4, 3, "Profesional"),
                    new AsignaturaSeed("Logistica Basica", 4, 3, "Profesional"),
                    new AsignaturaSeed("Seguridad Industrial", 3, 2, "Profesional"),
                    new AsignaturaSeed("Costos de Produccion", 4, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Inventarios", 5, 3, "Profesional"),
                    new AsignaturaSeed("Lean Manufacturing", 5, 3, "Profesional"),
                    new AsignaturaSeed("Ergonomia", 3, 2, "Profesional"),
                    new AsignaturaSeed("Proyecto de Produccion I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto de Produccion II", 6, 3, "Profesional")
                },
                "I-ELEC" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Circuitos Electricos", 2, 3, "Profesional"),
                    new AsignaturaSeed("Electromagnetismo", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Analogica", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Digital", 3, 3, "Profesional"),
                    new AsignaturaSeed("Sistemas Embebidos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Control Automatico", 4, 3, "Profesional"),
                    new AsignaturaSeed("Procesamiento Digital de Senales", 5, 3, "Profesional"),
                    new AsignaturaSeed("Comunicaciones", 5, 3, "Profesional"),
                    new AsignaturaSeed("Instrumentacion Industrial", 4, 3, "Profesional"),
                    new AsignaturaSeed("Electronica de Potencia", 5, 3, "Profesional"),
                    new AsignaturaSeed("Diseno de PCB", 5, 2, "Profesional"),
                    new AsignaturaSeed("Proyecto Electronico I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Electronico II", 6, 3, "Profesional")
                },
                "I-TEL" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Teoria de Comunicaciones", 3, 3, "Profesional"),
                    new AsignaturaSeed("Redes de Computadores", 3, 3, "Profesional"),
                    new AsignaturaSeed("Comunicaciones Digitales", 4, 3, "Profesional"),
                    new AsignaturaSeed("Sistemas Inalambricos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Conmutacion y Senalizacion", 4, 3, "Profesional"),
                    new AsignaturaSeed("Antenas y Propagacion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Transmision de Datos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Redes de Nueva Generacion", 5, 3, "Profesional"),
                    new AsignaturaSeed("Seguridad en Redes", 5, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Servicios", 5, 2, "Profesional"),
                    new AsignaturaSeed("Proyecto Telecom I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Telecom II", 6, 3, "Profesional"),
                    new AsignaturaSeed("Arquitecturas WAN", 5, 3, "Profesional")
                },
                "I-EMEC" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Mecanica de Materiales", 3, 3, "Profesional"),
                    new AsignaturaSeed("Termodinamica", 3, 3, "Profesional"),
                    new AsignaturaSeed("Maquinas Electricas", 4, 3, "Profesional"),
                    new AsignaturaSeed("Control de Motores", 5, 3, "Profesional"),
                    new AsignaturaSeed("Sistemas Hidraulicos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Diseno de Mecanismos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Conversion de Energia", 5, 3, "Profesional"),
                    new AsignaturaSeed("Mantenimiento Predictivo", 5, 3, "Profesional"),
                    new AsignaturaSeed("Automatizacion Industrial", 5, 3, "Profesional"),
                    new AsignaturaSeed("Diseno de Sistemas Electromecanicos", 6, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Electromecanico I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Electromecanico II", 6, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Activos Industriales", 5, 2, "Profesional")
                },
                "I-MECAT" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Diseno Mecanico", 3, 3, "Profesional"),
                    new AsignaturaSeed("Mecanica de Materiales", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Analogica", 3, 3, "Profesional"),
                    new AsignaturaSeed("Electronica Digital", 3, 3, "Profesional"),
                    new AsignaturaSeed("Sistemas Embebidos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Control Automatico", 4, 3, "Profesional"),
                    new AsignaturaSeed("Sensores y Actuadores", 4, 3, "Profesional"),
                    new AsignaturaSeed("Modelado y Simulacion", 4, 3, "Profesional"),
                    new AsignaturaSeed("Robotica", 5, 3, "Profesional"),
                    new AsignaturaSeed("Vision Artificial", 5, 3, "Profesional"),
                    new AsignaturaSeed("Automatizacion Industrial", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Mecatronico I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto Mecatronico II", 6, 3, "Profesional")
                },
                "I-PROD" => new List<AsignaturaSeed>
                {
                    new AsignaturaSeed("Investigacion de Operaciones", 3, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Operaciones", 4, 3, "Profesional"),
                    new AsignaturaSeed("Planeacion de la Produccion", 4, 3, "Profesional"),
                    new AsignaturaSeed("Logistica y Cadena de Suministro", 5, 3, "Profesional"),
                    new AsignaturaSeed("Gestion de Calidad", 4, 3, "Profesional"),
                    new AsignaturaSeed("Ingenieria Economica", 4, 3, "Profesional"),
                    new AsignaturaSeed("Costos y Presupuestos", 4, 3, "Profesional"),
                    new AsignaturaSeed("Simulacion de Sistemas", 5, 3, "Profesional"),
                    new AsignaturaSeed("Ergonomia y Seguridad", 4, 2, "Profesional"),
                    new AsignaturaSeed("Gestion de Proyectos", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto de Produccion I", 5, 3, "Profesional"),
                    new AsignaturaSeed("Proyecto de Produccion II", 6, 3, "Profesional"),
                    new AsignaturaSeed("Analisis de Datos para Operaciones", 5, 3, "Profesional")
                },
                _ => new List<AsignaturaSeed>()
            };
        }

        private sealed class StudentSeedPlan
        {
            public StudentSeedPlan(Estudiante estudiante, int startPeriodIndex, int semestres)
            {
                Estudiante = estudiante;
                StartPeriodIndex = startPeriodIndex;
                Semestres = semestres;
            }

            public Estudiante Estudiante { get; }
            public int StartPeriodIndex { get; }
            public int Semestres { get; }
        }

        private sealed class AsignaturaSeed
        {
            public AsignaturaSeed(string nombre, int semestre, int creditos, string tipo)
            {
                Nombre = nombre;
                Semestre = semestre;
                Creditos = creditos;
                Tipo = tipo;
            }

            public string Nombre { get; }
            public int Semestre { get; }
            public int Creditos { get; }
            public string Tipo { get; }
        }
    }
}
