using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAE.Migrations
{
    /// <inheritdoc />
    public partial class RenameUsuContraseñaToUsuContrasena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFacturas_Facturas_FacturaId",
                table: "DetalleFacturas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFacturas_Productos_ProductoId",
                table: "DetalleFacturas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Productos_ProductoId",
                table: "DetalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Ventas_VentaId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorProductos_Productos_ProductoId",
                table: "ProveedorProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorProductos_Proveedores_ProveedorId",
                table: "ProveedorProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_UsuId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_UsuId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_ProveedorProductos_ProductoId",
                table: "ProveedorProductos");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_VentaId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleFacturas_FacturaId",
                table: "DetalleFacturas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleFacturas_ProductoId",
                table: "DetalleFacturas");

            migrationBuilder.RenameColumn(
                name: "UsuContraseña",
                table: "Usuarios",
                newName: "UsuContrasena");

            migrationBuilder.AlterColumn<string>(
                name: "UsuId",
                table: "Ventas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuContrasena",
                table: "Usuarios",
                newName: "UsuContraseña");

            migrationBuilder.AlterColumn<string>(
                name: "UsuId",
                table: "Ventas",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UsuId",
                table: "Ventas",
                column: "UsuId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorProductos_ProductoId",
                table: "ProveedorProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_VentaId",
                table: "Facturas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_FacturaId",
                table: "DetalleFacturas",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_ProductoId",
                table: "DetalleFacturas",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFacturas_Facturas_FacturaId",
                table: "DetalleFacturas",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "FacturaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFacturas_Productos_ProductoId",
                table: "DetalleFacturas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Productos_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Ventas_VentaId",
                table: "Facturas",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorProductos_Productos_ProductoId",
                table: "ProveedorProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorProductos_Proveedores_ProveedorId",
                table: "ProveedorProductos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "RolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_UsuId",
                table: "Ventas",
                column: "UsuId",
                principalTable: "Usuarios",
                principalColumn: "UsuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
