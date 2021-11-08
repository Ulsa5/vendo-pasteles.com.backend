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
    public class MovimientosController : ControllerBase
    {
        private readonly ApplicationDbContext _context; //Para la inyeccion de dependencias, ya que en el método ApplicationDbContext es donde están las entidades instanciadas

        public MovimientosController(ApplicationDbContext context)
        {
            _context = context; //Inicialización del ApplicationDbContext
        }

        //Para obtener el listado de movimientos almacenados en la base de datos
        //GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.Include(p => p.Productos).Include(p=>p.Proveedores).ToListAsync();
        }

        //Obtener un registro específico de acuerdo a su id o llave primaria
        //GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {

            var movimiento = await _context.Movimientos.Include(p => p.Productos).Include(p=>p.Proveedores).FirstOrDefaultAsync(p => p.IdMovimiento == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        //Método utilizado para crear un registro en la base de datos
        //POST: api/Movimientos
        [HttpPost]
        public async Task<ActionResult<Movimiento>> PostMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimiento", new { id = movimiento.IdMovimiento }, movimiento);
        }

        //Método utilizado para actualizar un registro de acuerdo a su número de id o llave primaria
        //PUT: api/Movimientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.IdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
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
        //DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return Content("Registro Eliminado correctamente!");
        }

        //Método de apoyo para buscar un registro por su id o llave primaria
        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.IdMovimiento == id);
        }
    }
}
