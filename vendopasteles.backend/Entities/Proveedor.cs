using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vendopasteles.backend.Entities
{
    public partial class Proveedor
    {
        [Key] //llave primaria de la tabla
        public int IdProveedor { get; set; }
        [Required]
        public int NitProveedor { get; set; }
        [Required]
        public string NombreProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public int TelefonoProveedor { get; set; }
        public int? IdMovimiento { get; set; } //? = no puede ser nulo (nullable=false)
        [JsonIgnore] //evita que entremos en un bucle donde el proveedor llama al movimiento y este a su vez a un proveedor
        public virtual Movimiento IdMovimientoNavigation { get; set; } //se coloca como virtual para que no se almacene en una variable obligatoria
    }
}
