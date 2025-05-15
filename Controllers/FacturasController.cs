using Microsoft.AspNetCore.Mvc;
using BackendAE.Data;
using BackendAE.Models;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System.Linq;
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

        [HttpPost("factura/{ventaId}")]
        public IActionResult GenerarFacturaPDF(int ventaId, [FromBody] FacturaRequest request)
        {
            var venta = _context.Ventas
                .Include(v => v.Producto)
                .Include(v => v.Usuario)
                .FirstOrDefault(v => v.VentaId == ventaId);

            if (venta == null) return NotFound("Venta no encontrada");

            var descripcion = request.Descripcion;

            using var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
            var fontText = new XFont("Arial", 12, XFontStyle.Regular);

            double yPoint = 40;

            gfx.DrawString("A&E LIBRERÍA FACTURA ELECTRÓNICA", fontTitle, XBrushes.Black, new XRect(0, yPoint, page.Width, 30), XStringFormats.TopCenter);
            yPoint += 50;

            gfx.DrawString($"Fecha: {venta.VentaFecha:d}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Usuario: {venta.Usuario.UsuPNombre}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Producto: {venta.Producto.ProductoNombre}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Cantidad: {venta.ProductoCantidad}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Descripción: {venta.Descripcion}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Precio Unitario: Q.{venta.Producto.ProductoPrecio:F2}", fontText, XBrushes.Black, 40, yPoint); yPoint += 25;
            gfx.DrawString($"Total: Q.{venta.VentaTotal:F2}", fontText, XBrushes.Black, 40, yPoint);

            using var stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", $"Factura_Venta_{ventaId}.pdf");
        }
    }
}
