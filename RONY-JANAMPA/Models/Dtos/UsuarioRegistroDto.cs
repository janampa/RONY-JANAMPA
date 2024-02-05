using System.ComponentModel.DataAnnotations;

namespace RONY_JANAMPA.Models.Dtos
{
    public class UsuarioRegistroDto
    {
        
        [Required(ErrorMessage ="El Usuario es Obligatorio")]
        public String NombreUsuario { get; set; }
        [Required(ErrorMessage = "El Nombre Completo es Obligatorio")]
        public String NombreCompleto { get; set; }
        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

        public String Rol { get; set; }
    }
}
