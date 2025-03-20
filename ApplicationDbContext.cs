using Microsoft.EntityFrameworkCore;
using BackendAE.Models; // Asegúrate de que los modelos están en esta ruta

namespace BackendAE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definir las tablas de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorProducto> ProveedorProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DetalleFactura>()
        .Property(d => d.DetFacturaPrecioUnitario)
        .HasPrecision(18, 2);
            modelBuilder.Entity<DetalleVenta>()
       .Property(d => d.DetVentaPrecioUnidad)
       .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.DetVentaSubTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Factura>()
                .Property(f => f.FacturaTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.VentaTotal)
                .HasPrecision(18, 2);


            // Definir relaciones si es necesario
            modelBuilder.Entity<ProveedorProducto>()
                .HasIndex(p => new { p.ProveedorId, p.ProductoId })
                .IsUnique(); // Evita duplicados en la relación M:N

        }

    }
}
