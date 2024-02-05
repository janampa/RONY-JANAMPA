using System.ComponentModel.DataAnnotations;

namespace RONY_JANAMPA.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public String NombreUsuario { get; set; }
        public String NombreCompleto { get; set; }
        public string Password { get; set; }
        public String Rol {  get; set; }
    }
}
