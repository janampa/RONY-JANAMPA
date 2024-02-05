using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RONY_JANAMPA.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListaProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRecepcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDespacho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repartidor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
