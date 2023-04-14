using Microsoft.EntityFrameworkCore;

namespace WebApiUsuarios.Models
{
    public class UsuarioContext:DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext>options):base(options) { }
    }
}
