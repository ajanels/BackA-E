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
    public class DetalleFacturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalleFacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFactura>>> GetDetalleFacturas()
        {
            return await _context.DetalleFacturas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detalleFactura)
        {
            _context.DetalleFacturas.Add(detalleFactura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDetalleFacturas), new { id = detalleFactura.DetFacturaId }, detalleFactura);
        }
    }
}
