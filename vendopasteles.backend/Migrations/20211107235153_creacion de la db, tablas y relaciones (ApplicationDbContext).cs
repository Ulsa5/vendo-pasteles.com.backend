using Microsoft.EntityFrameworkCore.Migrations;

namespace vendopasteles.backend.Migrations
{
    public partial class creaciondeladbtablasyrelacionesApplicationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    IdMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoMovimiento = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.IdMovimiento);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    PrecioProducto = table.Column<int>(type: "int", nullable: false),
                    IdMovimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_IdMovimiento_Producto",
                        column: x => x.IdMovimiento,
                        principalTable: "Movimientos",
                        principalColumn: "IdMovimiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NitProveedor = table.Column<int>(type: "int", nullable: false),
                    NombreProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionProveedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoProveedor = table.Column<int>(type: "int", nullable: false),
                    IdMovimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                    table.ForeignKey(
                        name: "FK_IdMovimiento_Proveedor",
                        column: x => x.IdMovimiento,
                        principalTable: "Movimientos",
                        principalColumn: "IdMovimiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CodigoMovimiento",
                table: "Movimientos",
                column: "CodigoMovimiento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoProducto",
                table: "Productos",
                column: "CodigoProducto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdMovimiento",
                table: "Productos",
                column: "IdMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IdMovimiento",
                table: "Proveedores",
                column: "IdMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_NitProveedor",
                table: "Proveedores",
                column: "NitProveedor",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Movimientos");
        }
    }
}
