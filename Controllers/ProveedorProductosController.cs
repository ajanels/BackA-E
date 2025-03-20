using Microsoft.AspNetCore.Mvc;
using BackendAE.Data;
using BackendAE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProveedorProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorProducto>>> GetProveedorProductos()
        {
            return await _context.ProveedorProductos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorProducto>> PostProveedorProducto(ProveedorProducto proveedorProducto)
        {
            _context.ProveedorProductos.Add(proveedorProducto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProveedorProductos), new { id = proveedorProducto.ProveedorProductoId }, proveedorProducto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedorProducto(int id)
        {
            var proveedorProducto = await _context.ProveedorProductos.FindAsync(id);
            if (proveedorProducto == null) return NotFound();

            _context.ProveedorProductos.Remove(proveedorProducto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
