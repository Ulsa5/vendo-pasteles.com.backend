using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vendopasteles.backend.Contexts;
using vendopasteles.backend.Entities;

namespace vendopasteles.backend.Controllers
{
    //Anotaciones que le indican a Visual Studio que lo que viene es un api
    [ApiController]
    [Route("api/[Controller]")] //ruta para el endpoint, en este caso quedaría /api/Productos
    public class ProductosController:ControllerBase
    {
        private readonly ApplicationDbContext _context; //Para la inyeccion de dependencias, ya que en el método ApplicationDbContext es donde están las entidades instanciadas

        public ProductosController(ApplicationDbContext context)
        {
            _context = context; //Inicialización del ApplicationDbContext
        }

        //Para obtener el listado de productos almacenados en la base de datos
        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        //Obtener un registro específico de acuerdo a su id o llave primaria
        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }


        //Método utilizado para crear un registro en la base de datos
        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = producto.IdProducto }, producto);
        }

        //Método utilizado para actualizar un registro de acuerdo a su número de id o llave primaria
        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Registro Actualizado correctamente!");
        }

        //Método para eliminar un registro de acuerdo a su número de id o llave principal
        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Content("Registro Eliminado correctamente!");
        }

        //Método de apoyo para buscar un registro por su id o llave primaria
        private bool ProveedorExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
