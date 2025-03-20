using System.ComponentModel.DataAnnotations;

namespace BackendAE.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProveedorNombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProveedorDireccion { get; set; }

        [Required]
        [MaxLength(15)]
        public string ProveedorTelefono { get; set; }

        [MaxLength(100)]
        public string? ProveedorEmail { get; set; }

        [Required]
        public char ProveedorEstado { get; set; }
    }
}
