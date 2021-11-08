using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vendopasteles.backend.Entities
{
    public partial class Producto
    {
        [Key] //llave primaria de la tabla
        public int IdProducto { get; set; }
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadProducto { get; set; }
        public int PrecioProducto { get; set; }
        public int? IdMovimiento { get; set; } //? = no puede ser nulo (nullable=false)
        [JsonIgnore] //evita que entremos en un bucle donde el producto llama al movimiento y este a su vez a un producto
        public virtual Movimiento IdMovimientoNavigation { get; set; } //se coloca como virtual para que solo sirva para la relación
    }
}
