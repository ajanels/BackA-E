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
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFacturas), new { id = factura.FacturaId }, factura);
        }
    }
}
