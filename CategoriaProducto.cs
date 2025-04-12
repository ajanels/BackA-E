using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // <-- Agregar este using

namespace BackendAE.Models
{
    public class CategoriaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 👈 MUY IMPORTANTE
        public int CatProductoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CatProductoNombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string CatProductoDescripcion { get; set; }

        // Relación con Productos (IGNORAR en la serialización)
        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }
}
