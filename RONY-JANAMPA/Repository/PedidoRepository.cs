using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RONY_JANAMPA.Data;
using RONY_JANAMPA.Models;
using RONY_JANAMPA.Repository.IRepository;
using System;

namespace RONY_JANAMPA.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public bool CreatePedido(Pedido pedido)
        {
            
            pedido.FechaPedido = DateTime.Now.ToString("dd/MM/yyyy");
            pedido.FechaRecepcion = "";
            pedido.FechaDespacho = "";
            pedido.FechaEntrega = "";
            pedido.Estado = "Por Atender";
            _context.Pedido.Add(pedido);
            return SavePedido();
        }

        public bool ExisteID(int id)
        {
            return _context.Pedido.Any(c => c.Id == id);
        }

        public Pedido GetPedido(int id)
        {
            return _context.Pedido.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Pedido> GetPedidos()
        {
            return _context.Pedido.OrderBy(c=> c.Id).ToList();
        }

        public bool SavePedido()
        {
            return _context.SaveChanges()>=0 ? true:false;
        }

        public bool UpdatePedido(Pedido pedido)
        {
            Console.WriteLine("************> "+pedido.Estado);
            pedido.Vendedor = "MIREA";
            _context.Pedido.Update(pedido);
            return SavePedido();                
        }
    }
}
