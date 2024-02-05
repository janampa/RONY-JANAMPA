using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Models.Dtos;
using RONY_JANAMPA.Repository.IRepository;

namespace RONY_JANAMPA.Controllers
{
    //[Authorize(Roles ="admin")]
    [ApiController]
    [Route("api/Pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _peRepo;
        private readonly IMapper _mapper;
        public PedidoController(IPedidoRepository peRepo, IMapper mapper)
        {
            _peRepo = peRepo;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPedido()
        {
            var listaPedido = _peRepo.GetPedidos();
            var listaCategoriasDto = new List<PedidoDto>();
            foreach (var lista in listaPedido)
            {
                listaCategoriasDto.Add(_mapper.Map<PedidoDto>(lista));
            }
            return Ok(listaCategoriasDto);
        }
        [AllowAnonymous]
        [HttpGet("{Id}",Name = "GetPedido")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetPedido(int Id)
        {
            var itemPelicula = _peRepo.GetPedido(Id);
            if (itemPelicula == null)
            {
                return NotFound();
            }
            var itemPedido = _mapper.Map<PedidoDto>(itemPelicula);
            return Ok(itemPedido);
        }
        //[Authorize(Roles ="admin")]
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type =typeof(PedidoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreatePedido([FromBody] PedidoDto crearPedidoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(crearPedidoDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_peRepo.ExisteID(crearPedidoDto.Id))
            {
                ModelState.AddModelError("", "El pedido ya fue creado");
                return StatusCode(400,ModelState);
            }
            var pedido = _mapper.Map<Pedido>(crearPedidoDto);
            if (!_peRepo.CreatePedido(pedido))
            {
                ModelState.AddModelError("", $"algo salió mal al momento de crear{pedido.Id}");
                return StatusCode(500,ModelState);

            }
            return CreatedAtRoute("Getpedido", new {Id=pedido.Id}, pedido);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{Id}", Name = "UpdatePatchPedido")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdatePatchPedido(int Id, [FromBody] PedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pedidoDto == null || Id != pedidoDto.Id)
            {
                return BadRequest(ModelState);
            }
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            if(!_peRepo.UpdatePedido(pedido))
            {
                ModelState.AddModelError("", $"Algo salió mal al momento de actualizarel Registro {pedido.Id}");
                return StatusCode(500,ModelState); 
            }
            return NoContent(); 
        }
    }
}
