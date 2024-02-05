using AutoMapper;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Models.Dtos;

namespace RONY_JANAMPA.RetoMapper
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<Pedido, PedidoCreateDto>().ReverseMap();

        }
    }
}
