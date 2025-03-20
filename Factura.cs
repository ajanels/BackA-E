using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FacturaNumero { get; set; }

        public DateTime FacturaFecha { get; set; } = DateTime.Now;

        public decimal FacturaTotal { get; set; }

        [Required]
        [MaxLength(10)]
        public string FacturaEstado { get; set; }

        public int VentaId { get; set; }

        //[ForeignKey("VentaId")]
        //public virtual Venta Venta { get; set; }
    }
}
