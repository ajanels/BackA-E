using System.ComponentModel.DataAnnotations;

namespace BackendAE.Models
{
    public class Rol
    {
        //[Key]
        public int RolId { get; set; }

        //[Required]
        [MaxLength(100)]
        public string RolNombre { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        //public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
