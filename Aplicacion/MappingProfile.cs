using AutoMapper;
using tienda.microservicios.autor.api.modelo;

namespace tienda.microservicios.autor.api.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap< AutorLibro, AutorDto>();
        }
    }
}
