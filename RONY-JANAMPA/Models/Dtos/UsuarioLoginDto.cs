using System.ComponentModel.DataAnnotations;

namespace RONY_JANAMPA.Models.Dtos
{
    public class UsuarioLoginDto
    {
        
        [Required(ErrorMessage ="El Usuario es Obligatorio")]
        public String NombreUsuario { get; set; }

        [Required(ErrorMessage = "El Nombre Completo es Obligatorio")]
        public string Password { get; set; }
    }
}
