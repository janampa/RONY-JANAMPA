namespace RONY_JANAMPA.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string ListaProducto { get; set; }
        public string FechaPedido { get; set; }
        public string FechaRecepcion { get; set; }
        public string FechaDespacho { get; set; }
        public string FechaEntrega { get; set; }
        public string Vendedor {  get; set; }
        public string Repartidor {  get; set; }
        public string Estado {  get; set; }
        
    }
}
