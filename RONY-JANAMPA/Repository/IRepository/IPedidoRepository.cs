using Microsoft.AspNetCore.Components.Web;
using RONY_JANAMPA.Models;

namespace RONY_JANAMPA.Repository.IRepository
{
    public interface IPedidoRepository
    {
        ICollection<Pedido> GetPedidos();
        Pedido GetPedido(int id);
        bool ExisteID(int id);
        bool CreatePedido(Pedido pedido);

        bool UpdatePedido(Pedido pedido);

        bool SavePedido();
    }
}
