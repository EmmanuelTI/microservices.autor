using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using tienda.microservicios.autor.api.modelo;
using tienda.microservicios.autor.api.persisntencia;

namespace tienda.microservicios.autor.api.Aplicacion
{
    public class ConsultarNombre
    {
        public class AutorPorNombre : IRequest<AutorDto>
        {
            public string Nombre { get; set; }
        }

        public class Manejador : IRequestHandler<AutorPorNombre, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorPorNombre request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibros
                    .Where(p => p.Nombre == request.Nombre)
                    .FirstOrDefaultAsync(cancellationToken);

                if (autor == null)
                {
                    throw new Exception("No se encontró el autor con el nombre proporcionado");
                }

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
