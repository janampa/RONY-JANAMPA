using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using RONY_JANAMPA.Data;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Models.Dtos;
using RONY_JANAMPA.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace RONY_JANAMPA.Repository
{
    public class UsuariosRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private string claveSecreta;
        public UsuariosRepository(ApplicationDbContext contex, IConfiguration config)
        {
            _context = contex;
            claveSecreta = config.GetValue<string>("Apisettings:secreata");
        }
        public ICollection<Usuarios> GetUsuarios()
        {
            return _context.Usuarios.OrderBy(c => c.NombreUsuario).ToList();
        }

        public Usuarios GetUsuario(int id)
        {
            return _context.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public bool IsUniqueUser(string usuarios)
        {
            var  usuarioDB = _context.Usuarios.FirstOrDefault(u=> u.NombreUsuario==usuarios);
            if(usuarioDB == null)
            {
                return true;
            }
            return false;
        }

        

        public async Task<Usuarios> Registros(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEcriptado = obtenermd5(usuarioRegistroDto.Password);
            Usuarios usuarios = new Usuarios()
            {
            NombreUsuario = usuarioRegistroDto.NombreUsuario,
            Password = passwordEcriptado,
            NombreCompleto = usuarioRegistroDto.NombreCompleto,
            Rol = usuarioRegistroDto.Rol
            
            };
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();
            usuarios.Password = passwordEcriptado;
            return usuarios;
        }

        public async Task<usuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEcriptado = obtenermd5(usuarioLoginDto.Password);

            var usuarios = _context.Usuarios.FirstOrDefault(
                u=> u.NombreUsuario.ToLower()==usuarioLoginDto.NombreUsuario.ToLower()
                && u.Password==passwordEcriptado);

            //validar si el usuario no existe 

            if (usuarios == null)
            {
                return new usuarioLoginRespuestaDto()
                {
                    Token ="",
                    Usuario = null
                };
            }
            //si existe el suario
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarios.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarios.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = manejadorToken.CreateToken(tokenDescriptor);

            usuarioLoginRespuestaDto usuarioLoginRespuestaDto = new usuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuarios.NombreUsuario
            };

            return usuarioLoginRespuestaDto;
        }

        //MÉTODO PARA ENCRIPTAR

        public static string obtenermd5( string valor)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = md5.ComputeHash(data);
            string resp = "";
            for (int i=0; i<data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }

    }
}
