﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAE.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductoNombre { get; set; }

        public string? ProductoDescripcion { get; set; }

        [Required]
        public DateTime ProductoFecIngreso { get; set; }

        public DateTime ProductoFecVencimiento { get; set; }

        [Required]
        public int ProductoStock { get; set; }

        [Required]
        public int CatProductoId { get; set; }

        [ForeignKey("CatProductoId")]
        public virtual CategoriaProducto? CategoriaProducto { get; set; }
    }
}
