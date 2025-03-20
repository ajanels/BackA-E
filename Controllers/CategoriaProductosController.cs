using Microsoft.AspNetCore.Mvc;
using BackendAE.Data;
using BackendAE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProducto>>> GetCategorias()
        {
            return await _context.CategoriaProductos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProducto>> GetCategoria(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null) return NotFound();
            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoria(CategoriaProducto categoria)
        {
            _context.CategoriaProductos.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.CatProductoId }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaProducto categoria)
        {
            if (id != categoria.CatProductoId) return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null) return NotFound();

            _context.CategoriaProductos.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
