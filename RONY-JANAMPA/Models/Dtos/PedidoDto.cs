using System.ComponentModel.DataAnnotations;

namespace RONY_JANAMPA.Models.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Lista Obligatorio")]
        public string ListaProducto { get; set; }
        public string FechaPedido { get; set; }
        public string FechaRecepcion { get; set; }
        public string FechaDespacho { get; set; }
        public string FechaEntrega { get; set; }
        [Required(ErrorMessage = "Vendedor Obligatorio")]
        public string Vendedor { get; set; }
        [Required(ErrorMessage = "Repatidor Obligatorio")]
        public string Repartidor { get; set; }
        [Required(ErrorMessage = "Estado Obligatorio")]
        public string Estado { get; set; }

    }
}
