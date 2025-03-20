using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class DetalleVenta
    {
        [Key]
        public int DetVentaId { get; set; }

        [Required]
        public int DetVentaCantidad { get; set; }

        [Required]
        public decimal DetVentaPrecioUnidad { get; set; }

        [Required]
        public decimal DetVentaSubTotal { get; set; }

        [Required]
        public int VentaId { get; set; }

       // [ForeignKey("VentaId")]
        //public virtual Venta? Venta { get; set; }

        [Required]
        public int ProductoId { get; set; }

        //[ForeignKey("ProductoId")]
        //public virtual Producto? Producto { get; set; }
    }
}
