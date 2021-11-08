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
    [Route("api/[Controller]")] //ruta para el endpoint, en este caso quedaría /api/Movimientos
    public class ProveedoresController:ControllerBase
    {
        private readonly ApplicationDbContext _context;//Para la inyeccion de dependencias, ya que en el método ApplicationDbContext es donde están las entidades instanciadas

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context; //Inicialización del ApplicationDbContext
        }

        //Para obtener el listado de proveedores almacenados en la base de datos
        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        //Obtener un registro específico de acuerdo a su id o llave primaria
        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        //Método utilizado para crear un registro en la base de datos
        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedores", new { id = proveedor.IdProveedor }, proveedor);
        }

        //Método utilizado para actualizar un registro de acuerdo a su número de id o llave primaria
        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

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
        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Content("Registro Eliminado correctamente!");
        }

        //Método de apoyo para buscar un registro por su id o llave primaria
        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}
