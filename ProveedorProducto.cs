using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class ProveedorProducto
    {
        [Key]
        public int ProveedorProductoId { get; set; }

        public int ProveedorId { get; set; }

        // [ForeignKey("ProveedorId")]
        //public virtual Proveedor Proveedor { get; set; }

        public int ProductoId { get; set; }

        // [ForeignKey("ProductoId")]
        //public virtual Producto Producto { get; set; }
    }
}
