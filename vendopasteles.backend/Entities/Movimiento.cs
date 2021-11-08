using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vendopasteles.backend.Entities
{
    public partial class Movimiento
    {
        public Movimiento()
        {
            //la tabla inicializa los valores que serán cargados en los atributos virtuales (apoyo con las relaciones)
            Proveedores = new HashSet<Proveedor>();
            Productos = new HashSet<Producto>();
        }
        [Key] //llave primaria de la tabla
        public int IdMovimiento { get; set; }
        [Required]
        public int CodigoMovimiento { get; set; }
        [Required]
        public string FechaMovimiento { get; set; }
        [Required]
        public int CantidadProducto { get; set; }
        [Required]
        public string TipoMovimiento { get; set; }
        public virtual ICollection<Producto> Productos { get; set; } //se coloca como virtual para que solo sirva para la relación
        public virtual ICollection<Proveedor> Proveedores { get; set; } //se coloca como virtual para que solo sirva para la relación
    }
}
