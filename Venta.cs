using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }

        [Required]
        public DateTime VentaFecha { get; set; } = DateTime.Now;

        [Required]
        public decimal VentaTotal { get; set; }

        [Required]
        public string UsuId { get; set; }

        //[ForeignKey("UsuId")]
        //public virtual Usuario? Usuario { get; set; }
    }
}
