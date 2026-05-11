# Prueba Técnica — API Académica Institucional

# Contexto

La universidad cuenta con múltiples sistemas internos:

- Sistema académico
- Sistema financiero
- Sistema de prácticas profesionales
- Sistema de biblioteca
- Sistema de analítica institucional

Actualmente, varios sistemas necesitan consultar información académica de estudiantes de manera centralizada y segura. Por ello, el área de arquitectura institucional ha decidido exponer una API Académica que funcione como fuente oficial de información académica.

Tu misión será desarrollar dicha API utilizando el modelo de datos proporcionado.

La API debe simular el comportamiento de un sistema institucional real, por lo que:

- Debe exponer información académica.
- Debe permitir consultas externas.
- Debe seguir buenas prácticas de ingeniería.
- Debe enfocarse en proveer datos confiables y bien estructurados.

---

# Objetivo Técnico

Construir una API RESTful robusta y escalable que permita consultar información académica institucional.

La API será consumida por sistemas externos, especialmente el sistema de prácticas profesionales.

---

# Tecnologías

## Backend

- ASP.NET Core Web API
- C#

---

# Base de datos

- SQL Server

---

# ORM

- Entity Framework

---

# Requisitos Técnicos Obligatorios

---

# 1. Arquitectura

La solución debe implementar una arquitectura profesional.

Se evaluará:

- Separación de responsabilidades
- Escalabilidad
- Mantenibilidad
- Organización del proyecto

## No se aceptará

- Toda la lógica en controllers
- Acceso directo a base de datos desde endpoints
- Código altamente acoplado
- Código sin separación de capas

---

# 2. Modelo de Datos

Se debe implementar el siguiente modelo académico.

---

# ENTIDADES

---

## FACULTAD

Representa las facultades de la universidad.

### Tabla FACULTAD

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| facultad_id | INT | ✅ |  | Identificador |
| codigo | VARCHAR(20) |  |  | Código institucional |
| nombre | VARCHAR(120) |  |  | Nombre de la facultad |
| estado | VARCHAR(20) |  |  | Activa/Inactiva |

---

## PROGRAMA_ACADEMICO

Carreras o programas ofertados por la universidad.

### Tabla PROGRAMA_ACADEMICO

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| programa_id | INT | ✅ |  | Identificador |
| facultad_id | INT |  | ✅ | Facultad |
| codigo | VARCHAR(20) |  |  | Código del programa |
| nombre | VARCHAR(150) |  |  | Nombre del programa |
| nivel | VARCHAR(30) |  |  | Tecnología, pregrado, especialización |
| creditos_totales | INT |  |  | Créditos requeridos |
| semestres | INT |  |  | Cantidad de semestres |
| estado | VARCHAR(20) |  |  | Activo/Inactivo |

---

## ESTUDIANTE

Entidad principal del sistema académico.

### Tabla ESTUDIANTE

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| estudiante_id | INT | ✅ |  | Identificador |
| programa_id | INT |  | ✅ | Programa académico |
| codigo_estudiantil | VARCHAR(30) |  |  | Código institucional |
| tipo_documento | VARCHAR(10) |  |  | CC/TI/CE |
| numero_documento | VARCHAR(30) |  |  | Documento |
| nombres | VARCHAR(100) |  |  | Nombres |
| apellidos | VARCHAR(100) |  |  | Apellidos |
| correo_institucional | VARCHAR(120) |  |  | Correo |
| telefono | VARCHAR(30) |  |  | Teléfono |
| fecha_ingreso | DATE |  |  | Ingreso a la universidad |
| estado_academico | VARCHAR(20) |  |  | Activo, suspendido, egresado |

---

## PERIODO_ACADEMICO

Representa periodos o semestres académicos.

### Tabla PERIODO_ACADEMICO

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| periodo_id | INT | ✅ |  | Identificador |
| anio | INT |  |  | Año |
| semestre | INT |  |  | Semestre |
| fecha_inicio | DATE |  |  | Inicio |
| fecha_fin | DATE |  |  | Fin |
| estado | VARCHAR(20) |  |  | Abierto/Cerrado |

---

## MATRICULA

Permite validar si el estudiante se encuentra inscrito oficialmente.

### Tabla MATRICULA

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| matricula_id | INT | ✅ |  | Identificador |
| estudiante_id | INT |  | ✅ | Estudiante |
| periodo_id | INT |  | ✅ | Periodo |
| fecha_matricula | DATE |  |  | Fecha |
| estado | VARCHAR(20) |  |  | Activa, cancelada, aplazada |

---

## PLAN_ESTUDIO

Representa el pensum académico.

### Tabla PLAN_ESTUDIO

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| plan_id | INT | ✅ |  | Identificador |
| programa_id | INT |  | ✅ | Programa |
| version | VARCHAR(20) |  |  | Versión del plan |
| estado | VARCHAR(20) |  |  | Activo/Inactivo |

---

## ASIGNATURA

Materias del programa académico.

### Tabla ASIGNATURA

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| asignatura_id | INT | ✅ |  | Identificador |
| plan_id | INT |  | ✅ | Plan de estudio |
| codigo | VARCHAR(20) |  |  | Código |
| nombre | VARCHAR(120) |  |  | Nombre |
| creditos | INT |  |  | Créditos |
| semestre_recomendado | INT |  |  | Semestre sugerido |
| tipo | VARCHAR(30) |  |  | Obligatoria/Electiva |

---

## HISTORIAL_ACADEMICO

Historial de materias cursadas por el estudiante.

### Tabla HISTORIAL_ACADEMICO

| Campo | Tipo | PK | FK | Descripción |
|---|---|---|---|---|
| historial_id | INT | ✅ |  | Identificador |
| estudiante_id | INT |  | ✅ | Estudiante |
| asignatura_id | INT |  | ✅ | Asignatura |
| periodo_id | INT |  | ✅ | Periodo |
| nota_final | DECIMAL(3,2) |  |  | Nota final |
| estado | VARCHAR(20) |  |  | Aprobada/Reprobada |
| creditos_aprobados | INT |  |  | Créditos obtenidos |

---

# Relaciones Principales

| Relación | Cardinalidad |
|---|---|
| FACULTAD → PROGRAMA_ACADEMICO | 1:N |
| PROGRAMA_ACADEMICO → ESTUDIANTE | 1:N |
| ESTUDIANTE → MATRICULA | 1:N |
| PERIODO_ACADEMICO → MATRICULA | 1:N |
| PROGRAMA_ACADEMICO → PLAN_ESTUDIO | 1:N |
| PLAN_ESTUDIO → ASIGNATURA | 1:N |
| ESTUDIANTE → HISTORIAL_ACADEMICO | 1:N |
| ASIGNATURA → HISTORIAL_ACADEMICO | 1:N |
| PERIODO_ACADEMICO → HISTORIAL_ACADEMICO | 1:N |

---

# 3. API REST

La API debe exponer endpoints RESTful correctamente diseñados.

---

# 4. Buenas prácticas obligatorias

La solución debe incluir:

- DTOs
- Validaciones
- Filtros
- Ordenamiento
- Migraciones
- Seeders

---

# 5. Seguridad

## JWT Authentication

Debe existir autenticación institucional.

---

## Roles mínimos

| Rol | Permisos |
|---|---|
| admin | Acceso total |
| academic_consumer | Solo lectura |

---

# 6. Swagger/OpenAPI

Debe documentar:

- Endpoints
- Requests
- Responses
- Autenticación

---

# Endpoints Requeridos

---

# AUTH

## POST /auth/login

Request:

```json
{
  "email": "admin@university.edu",
  "password": "123456"
}
```

Response:

```json
{
  "token": "jwt-token"
}
```

---

# FACULTADES

## GET /faculties

Lista paginada.

---

## GET /faculties/{id}

Detalle.

---

# PROGRAMAS

## GET /programs

Filtros:
- facultyId

---

## GET /programs/{id}

---

# ESTUDIANTES

## GET /students

Filtros:
- programId
- academicStatus

Debe soportar:
- paginación
- filtros
- ordenamiento

---

## GET /students/{id}

Debe retornar:
- información básica
- programa
- facultad

---

## GET /students/{id}/academic-summary

Debe retornar:
- créditos aprobados
- promedio
- estado académico
- matrícula vigente

---

## GET /students/{id}/enrollments

Historial de matrículas.

---

## GET /students/{id}/academic-history

Debe incluir:
- materia
- nota
- créditos
- periodo

---

# PERIODOS

## GET /periods

---

## GET /periods/current

---

# Seeders Obligatorios

La base de datos debe incluir mínimo:

- 3 facultades
- 5 programas académicos
- 50 estudiantes
- Historial académico coherente
- Matrículas activas e inactivas
- Datos realistas

---

# Restricciones Importantes

---

## 1. Los endpoints deben representar recursos

### Incorrecto

```http
GET /getAllStudents
```

### Correcto

```http
GET /students
```

---

## 2. La API no debe tomar decisiones de negocio externas

La API académica:
- NO valida prácticas
- NO valida graduaciones
- NO valida becas

Solo expone información académica.
