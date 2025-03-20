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
    public class DetalleVentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalleVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetalleVentas()
        {
            return await _context.DetalleVentas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DetalleVenta>> PostDetalleVenta(DetalleVenta detalleVenta)
        {
            _context.DetalleVentas.Add(detalleVenta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDetalleVentas), new { id = detalleVenta.DetVentaId }, detalleVenta);
        }
    }
}
