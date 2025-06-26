using Microsoft.EntityFrameworkCore;
using tienda.microservicios.autor.api.Aplicacion;
using tienda.microservicios.autor.api.persisntencia;
using MediatR;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configura Kestrel para usar el puerto dinámico
builder.WebHost.ConfigureKestrel(options =>
{
    var portEnv = Environment.GetEnvironmentVariable("PORT");
    var port = string.IsNullOrEmpty(portEnv) ? 5000 : int.Parse(portEnv);
    options.ListenAnyIP(port);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoAutor>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodos", policy =>
    {
        policy.AllowAnyOrigin()   // Permitir cualquier origen
              .AllowAnyHeader()   // Permitir cualquier encabezado
              .AllowAnyMethod();  // Permitir cualquier método (GET, POST, etc.)
    });
});


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirTodos");

app.UseAuthorization();

app.MapControllers();

app.Run();
