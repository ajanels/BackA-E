using System.ComponentModel.DataAnnotations;

namespace BackendAE.Models
{
    public class Rol
    {
        //[Key]
        public int RolId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RolNombre { get; set; }

        //public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
