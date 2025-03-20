using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class Usuario
    {
        [Key]
        [MaxLength(20)]
        public string UsuId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UsuPNombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string UsuPApellido { get; set; }

        [Required]
        [MaxLength(13)]
        public string UsuCui { get; set; }

        [MaxLength(10)]
        public string? UsuNit { get; set; }

        [Required]
        public DateTime UsuFecNacimiento { get; set; }

        public DateTime UsuFecIngreso { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string UsuDireccion { get; set; }

        [Required]
        [MaxLength(8)]
        public string UsuTelMovil { get; set; }

        [Required]
        public char UsuGenero { get; set; }

        [Required]
        public char UsuEstado { get; set; }

        [MaxLength(50)]
        public string UsuPuesto { get; set; }

        [Required]
        public int RolId { get; set; }

        ///[ForeignKey("RolId")]
       // public virtual Rol? Rol { get; set; }

        [Required]
        [MaxLength(255)]
        public string UsuContraseña { get; set; }

        [Required]
        [MaxLength(100)]
        public string UsuEmail { get; set; }
    }
}
