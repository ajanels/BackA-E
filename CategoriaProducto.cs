using System.ComponentModel.DataAnnotations;

namespace BackendAE.Models
{
    public class CategoriaProducto
    {
        [Key]
        public int CatProductoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CatProductoNombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string CatProductoDescripcion { get; set; }
    }
}
