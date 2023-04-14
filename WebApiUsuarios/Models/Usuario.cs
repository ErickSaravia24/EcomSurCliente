using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsuarios.Models
{
    [Table("BDUsuarios")]
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Amaterno { get; set; }
        public string? Apaterno { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
        public string? Colonia { get; set; }
        public string? Correro { get; set; }
        public string? Password { get; set; }
        public int IdRol { get; set; }

    }
}
