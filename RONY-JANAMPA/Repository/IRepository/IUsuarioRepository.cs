using Microsoft.AspNetCore.Components.Web;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Models.Dtos;

namespace RONY_JANAMPA.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuarios> GetUsuarios();
        Usuarios GetUsuario(int Id);
        bool IsUniqueUser(string usuarios);

        Task<usuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuarios> Registros(UsuarioRegistroDto usuarioRegistroDto);

    }
}
