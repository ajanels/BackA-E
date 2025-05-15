using BackendAE.Data;
using BackendAE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Include(p => p.CategoriaProducto) // Carga ansiosamente la relación CategoriaProducto
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.CategoriaProducto)
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (producto == null) return NotFound();
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId) return BadRequest();

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Charts
        [HttpGet("stock-resumen")]
        public async Task<ActionResult<IEnumerable<object>>> GetStockResumen()
        {
            var data = await _context.Productos
                .Select(p => new
                {
                    nombre = p.ProductoNombre,
                    stock = p.ProductoStock
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("disponibilidad")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductosDisponibilidad()
        {
            var disponibles = await _context.Productos.CountAsync(p => p.ProductoStock > 0);
            var agotados = await _context.Productos.CountAsync(p => p.ProductoStock == 0);

            var resultado = new[]
            {
            new { estado = "Disponibles", cantidad = disponibles },
            new { estado = "Agotados",   cantidad = agotados }
        };
            return Ok(resultado);
        }

        //[HttpGet("scategoria")]
        //public async Task<ActionResult<IEnumerable<object>>> GetStockPromedioPorCategoria()
        //{
        //    var data = await _context.Productos
        //        .Where(p => p.CategoriaProducto != null) // Evita categorías nulas
        //        .GroupBy(p => p.CategoriaProducto.CatProductoNombre)
        //        .Select(g => new
        //        {
        //            categoria = g.Key,
        //            promedioStock = g.Average(p => p.ProductoStock)
        //        })
        //        .ToListAsync();

        //    return Ok(data);
        //}
        [HttpGet("topstock")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopProductosPorStock()
        {
            var data = await _context.Productos
                .OrderByDescending(p => p.ProductoStock)
                .Take(5)
                .Select(p => new
                {
                    nombre = p.ProductoNombre,
                    stock = p.ProductoStock
                })
                .ToListAsync();

            return Ok(data);
        }

    }
}