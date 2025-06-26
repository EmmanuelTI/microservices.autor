using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using tienda.microservicios.autor.api.Aplicacion;
using tienda.microservicios.autor.api.persisntencia;


namespace tienda.microservicios.autor.api.extencions
{
    public static class ServiceColeccionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(cfg =>
                cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoAutor>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //agregamos MediatR como servicio

            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));


            return services;


        }
    }
    
}