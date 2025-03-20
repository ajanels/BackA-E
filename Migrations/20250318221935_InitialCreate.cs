using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProductos",
                columns: table => new
                {
                    CatProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatProductoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CatProductoDescripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProductos", x => x.CatProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProveedorDireccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProveedorTelefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProveedorEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProveedorEstado = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductoFecIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoFecVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoStock = table.Column<int>(type: "int", nullable: false),
                    CatProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Productos_CategoriaProductos_CatProductoId",
                        column: x => x.CatProductoId,
                        principalTable: "CategoriaProductos",
                        principalColumn: "CatProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UsuPNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UsuPApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UsuCui = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UsuNit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UsuFecNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuFecIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuDireccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsuTelMovil = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UsuGenero = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    UsuEstado = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    UsuPuesto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    UsuContraseña = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsuEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveedorProductos",
                columns: table => new
                {
                    ProveedorProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorProductos", x => x.ProveedorProductoId);
                    table.ForeignKey(
                        name: "FK_ProveedorProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedorProductos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VentaTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UsuId = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Ventas_Usuarios_UsuId",
                        column: x => x.UsuId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetVentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetVentaCantidad = table.Column<int>(type: "int", nullable: false),
                    DetVentaPrecioUnidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DetVentaSubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.DetVentaId);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaNumero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FacturaFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacturaTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FacturaEstado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Facturas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFacturas",
                columns: table => new
                {
                    DetFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetFacturaCantidad = table.Column<int>(type: "int", nullable: false),
                    DetFacturaPrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DetFacturaSubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFacturas", x => x.DetFacturaId);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_FacturaId",
                table: "DetalleFacturas",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_ProductoId",
                table: "DetalleFacturas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_VentaId",
                table: "Facturas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CatProductoId",
                table: "Productos",
                column: "CatProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorProductos_ProductoId",
                table: "ProveedorProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorProductos_ProveedorId_ProductoId",
                table: "ProveedorProductos",
                columns: new[] { "ProveedorId", "ProductoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UsuId",
                table: "Ventas",
                column: "UsuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleFacturas");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "ProveedorProductos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "CategoriaProductos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
