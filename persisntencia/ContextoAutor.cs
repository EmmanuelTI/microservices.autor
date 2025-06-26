using Microsoft.EntityFrameworkCore;
using tienda.microservicios.autor.api.modelo;

namespace tienda.microservicios.autor.api.persisntencia
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options)
        { 
        }

        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradosAcademicos { get; set; }
    }
}


