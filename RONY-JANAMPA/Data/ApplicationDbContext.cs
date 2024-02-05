using Microsoft.EntityFrameworkCore;
using RONY_JANAMPA.Models;

namespace RONY_JANAMPA.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        //AGREGAR MODELOS

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
