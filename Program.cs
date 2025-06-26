using Microsoft.EntityFrameworkCore;
using tienda.microservicios.autor.api.Aplicacion;
using tienda.microservicios.autor.api.persisntencia;
using MediatR;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoAutor>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // URL de tu aplicación React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registrar AutoMapper
// Aquí se registra AutoMapper buscando los perfiles en el ensamblado actual (el que tiene los handlers)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Registrar MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirReact");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
