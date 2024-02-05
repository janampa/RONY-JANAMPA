using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Models.Dtos;
using RONY_JANAMPA.Repository.IRepository;
using System.Net;

namespace RONY_JANAMPA.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usRepo;
        protected RespuestApi _respuestaApi;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioRepository usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            this._respuestaApi = new();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuario = _usRepo.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();
            foreach (var lista in listaUsuario)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listaUsuariosDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetUsuario(int Id)
        {
            var itemUsuario = _usRepo.GetUsuario(Id);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Registros([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validarNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioRegistroDto.NombreUsuario);
            if(!validarNombreUsuarioUnico)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessage.Add("Nombre de Usuario ya existe");
                return BadRequest(_respuestaApi);
            }
            var usuario = await _usRepo.Registros(usuarioRegistroDto);
            if (usuario == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessage.Add("Error en el registro");
                return BadRequest(_respuestaApi);
            }
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.isSuccess = true;
            _respuestaApi.ErrorMessage.Add("Registro Correcto");
            return Ok(_respuestaApi);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginoDto)
        {
            
            var respuestaLogin = await _usRepo.Login(usuarioLoginoDto);

            if (respuestaLogin == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessage.Add("Usuario  y/o Usuario Incorrecto");
                return BadRequest(_respuestaApi);
            }
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.isSuccess = true;
            _respuestaApi.Result= respuestaLogin;
            _respuestaApi.ErrorMessage.Add("Correcto");
            return Ok(_respuestaApi);
        }
    }
}
