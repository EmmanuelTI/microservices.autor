using FluentValidation;
using MediatR;
using tienda.microservicios.autor.api.modelo;
using tienda.microservicios.autor.api.persisntencia;
using static tienda.microservicios.autor.api.Aplicacion.Nuevo;


namespace tienda.microservicios.autor.api.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime FechaNacimiento { get; set; }

        }

        internal class Manejador
        {
        }
    }

    public class EjecutarValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutarValidacion()
        {
            RuleFor(p => p.Nombre).NotEmpty();
            RuleFor(p => p.Apellido).NotEmpty();
            RuleFor(p => p.FechaNacimiento).NotEmpty();
        }
    }
    public class Manejador : IRequestHandler<Ejecuta>
    {
        public readonly ContextoAutor _context;
        public Manejador(ContextoAutor context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var autorLibro = new AutorLibro
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Convert.ToString(Guid.NewGuid()),
            };
            _context.AutorLibros.Add(autorLibro);
            var respuesta = await _context.SaveChangesAsync();
            if (respuesta > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se puedo insertar el autor libro");
        }

    }


}


