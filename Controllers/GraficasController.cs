//namespace BackendAE.Controllers
//{
//    public class GraficasController
//    {
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendAE.Data;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraficasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GraficasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("precios-productos")]
        public async Task<ActionResult<IEnumerable<object>>> GetPreciosProductos()
        {
            var data = await _context.Productos
                .Select(p => new
                {
                    nombre = p.ProductoNombre,
                    precio = p.ProductoPrecio
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}

