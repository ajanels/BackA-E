using System;
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

        // Nuevo campo ProductoCantidad
        public int? ProductoCantidad { get; set; } // Coincide con la nulabilidad en la base de datos

        // Propiedad de clave foránea para relacionar con Producto
        [ForeignKey("Producto")] // Opcional: Especifica explícitamente el nombre de la propiedad de navegación
        public int? ProductoId { get; set; } // Debe ser nullable para coincidir con la configuración en la base de datos

        // Propiedad de navegación para acceder a la entidad Producto relacionada
        public virtual Producto Producto { get; set; }

        // Opcional: Propiedades de navegación para Usuario si tienes una entidad Usuario
        // [ForeignKey("UsuId")]
        // public virtual Usuario? Usuario { get; set; }
    }
}