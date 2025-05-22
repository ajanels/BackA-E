using BackendAE.Data;
using BackendAE.Dtos;
using BackendAE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas
                .Include(v => v.Producto)
                .Include(v => v.Usuario)
                .ToListAsync();
        }

        // GET: api/ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Producto)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta == null)
                return NotFound();

            return venta;
        }

        // POST: api/ventas
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta([FromBody] CrearVentaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producto = await _context.Productos.FindAsync(dto.ProductoId);
            if (producto == null)
                return BadRequest(new { error = "Producto no encontrado" });

            if (dto.ProductoCantidad > producto.ProductoStock)
                return BadRequest(new { error = "No hay suficiente stock disponible." });

            producto.ProductoStock -= dto.ProductoCantidad;
            _context.Entry(producto).State = EntityState.Modified;

            var venta = new Venta
            {
                ProductoId = dto.ProductoId,
                UsuId = dto.UsuId,
                ProductoCantidad = dto.ProductoCantidad,
                VentaTotal = dto.ProductoCantidad * producto.ProductoPrecio,
                VentaFecha = DateTime.Now,
                Descripcion = dto.Descripcion
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            var ventaConRelaciones = await _context.Ventas
                .Include(v => v.Producto)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.VentaId == venta.VentaId);

            return CreatedAtAction(nameof(GetVenta), new { id = venta.VentaId }, ventaConRelaciones);
        }

        // PUT: api/ventas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, [FromBody] EditarVentaDTO dto)
        {
            if (id != dto.VentaId)
                return BadRequest();

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
                return NotFound();

            venta.VentaFecha = dto.VentaFecha;
            venta.VentaTotal = dto.VentaTotal;
            venta.UsuId = dto.UsuId;
            venta.ProductoId = dto.ProductoId;
            venta.ProductoCantidad = dto.ProductoCantidad;
            venta.Descripcion = dto.Descripcion;

            _context.Entry(venta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
                return NotFound();

            if (venta.ProductoId.HasValue && venta.ProductoCantidad.HasValue)
            {
                var producto = await _context.Productos.FindAsync(venta.ProductoId.Value);
                if (producto != null)
                {
                    producto.ProductoStock += venta.ProductoCantidad.Value;
                    _context.Entry(producto).State = EntityState.Modified;
                }
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ventas/factura/5
        [HttpPost("factura/{ventaId}")]
        public async Task<IActionResult> GenerarFacturaPDF(int ventaId, [FromBody] FacturaRequest request)
        {
            var venta = await _context.Ventas
                .Include(v => v.Producto)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.VentaId == ventaId);

            if (venta == null)
                return NotFound("Venta no encontrada");

            using var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Logo
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                using var imageStream = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
                var image = XImage.FromStream(() => imageStream);
                gfx.DrawImage(image, 40, 20, 100, 100); // Posición (x, y) y tamaño
            }

            var fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
            var fontText = new XFont("Arial", 12, XFontStyle.Regular);

            double yPoint = 130;

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

        //Charts
        [HttpGet("ventasxdia")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerVentasPorDia()
        {
            var resultado = await _context.Ventas
                .GroupBy(v => v.VentaFecha.Date)
                .Select(g => new
                {
                    Fecha = g.Key.ToString("yyyy-MM-dd"),
                    TotalVentas = g.Sum(v => v.VentaTotal)
                })
                .ToListAsync();

            return Ok(resultado);
        }

        [HttpGet("masvendido")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerProductosMasVendidos()
        {
            var resultado = await _context.Ventas
                .Include(v => v.Producto)
                .GroupBy(v => v.Producto.ProductoNombre)
                .Select(g => new
                {
                    Producto = g.Key,
                    TotalCantidad = g.Sum(v => v.ProductoCantidad),
                    TotalVentas = g.Sum(v => v.VentaTotal)
                })
                .OrderByDescending(g => g.TotalCantidad)
                .ToListAsync();

            return Ok(resultado);
        }
        // GET: api/ventas/reporte/ventas-por-fecha
        //[HttpGet("ventasxfecha")]
        //public async Task<ActionResult<IEnumerable<object>>> ObtenerTotalVentasPorFecha()
        //{
        //    var resumen = await _context.Ventas
        //    .GroupBy(v => v.VentaFecha.Date)
        //    .ToListAsync(); // Ejecuta en SQL hasta aquí

        //    var resultado = resumen
        //        .Select(g => new {
        //            Fecha = g.Key.ToString("yyyy-MM-dd"),
        //            Total = g.Sum(e => e.VentaTotal)
        //        })
        //        .OrderBy(e => e.Fecha)
        //        .ToList();

        //    return Ok(resultado);

        //}
        //[HttpGet("stockvsproductos")]
        //public async Task<IActionResult> ObtenerPorcentajeVendidosVsStock()
        //{
        //    var data = await _context.Productos
        //        .Select(p => new
        //        {
        //            Producto = p.ProductoNombre,
        //            Vendido = _context.Ventas
        //                        .Where(v => v.ProductoId == p.ProductoId)
        //                        .Sum(v => (int?)v.VentaTotal) ?? 0,
        //            Stock = p.ProductoStock
        //        })
        //        .ToListAsync();

        //    var result = data.Select(d => new
        //    {
        //        d.Producto,
        //        Total = d.Vendido + d.Stock,
        //        Vendido = d.Vendido,
        //        PorcentajeVendido = (d.Vendido + d.Stock) > 0 ? Math.Round((double)d.Vendido / (d.Vendido + d.Stock) * 100, 2) : 0
        //    });

        //    return Ok(result);
        //}
        // GET: api/ventas/reporte/ventas-por-cantidad
        // GET: api/ventas/ventasxcantidad
        // GET: api/ventas/ventasxcantidad
        [HttpGet("ventasxcantidad")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerVentasPorCantidad()
        {
            // 1) Total general de ventas
            var totalGeneral = await _context.Ventas.SumAsync(v => v.VentaTotal);

            // 2) Agrupar y proyectar con propiedades en minúscula
            var data = await _context.Ventas
                .GroupBy(v => v.ProductoCantidad)
                .Select(g => new
                {
                    cantidad = g.Key,
                    numeroVentas = g.Count(),
                    totalVentas = g.Sum(v => v.VentaTotal),
                    porcentaje = totalGeneral == 0
                                     ? 0
                                     : Math.Round(g.Sum(v => v.VentaTotal) / totalGeneral * 100, 2)
                })
                .OrderBy(x => x.cantidad)
                .ToListAsync();

            return Ok(data);
        }
    }
}
