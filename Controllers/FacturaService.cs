using BackendAE.Models;

namespace BackendAE.Controllers
{
    public interface IFacturaService
    {
        MemoryStream GenerarFacturaPDF(Venta venta, string descripcion);
    }
}
