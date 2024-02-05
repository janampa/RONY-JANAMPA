using System.ComponentModel.DataAnnotations;

namespace RONY_JANAMPA.Models.Dtos
{
    public class UsuarioDto
    {
        
        public int Id { get; set; }
        public String NombreUsuario { get; set; }
        public String NombreCompleto { get; set; }
        public string Password { get; set; }
        public String Rol { get; set; }
    }
}
