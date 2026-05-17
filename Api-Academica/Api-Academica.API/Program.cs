using Api_Academica.DataAccess.Context;
using Api_Academica.DataAccess.Repositories;
using Api_Academica.Domain.Interfaces.Repositories;
using Api_Academica.Domain.Interfaces.Services;
using Api_Academica.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// controllers
builder.Services.AddControllers();
// dbcontext
builder.Services.AddDbContext<AcademicDBContext>(options => 
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
// services
builder.Services.AddScoped<IFacultadService, FacultadService>();
// repositories
builder.Services.AddScoped<IFacultadRepository,FacultadRepository>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
