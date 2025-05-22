using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class DetalleFactura
    {
        [Key]
        public int DetFacturaId { get; set; }

        public int DetFacturaCantidad { get; set; }

        public decimal DetFacturaPrecioUnitario { get; set; }

        public decimal DetFacturaSubTotal { get; set; }

        public int FacturaId { get; set; }

       // [ForeignKey("FacturaId")]
       // public virtual Factura Factura { get; set; }

        public int ProductoId { get; set; }

        //[ForeignKey("ProductoId")]
       // public virtual Producto Producto { get; set; }
    }
}
