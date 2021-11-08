using Microsoft.EntityFrameworkCore;
using vendopasteles.backend.Entities;

namespace vendopasteles.backend.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        //Instanciamos las entidades que serán migradas a base de datos
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {

        }

        /* En este método podemos decidir que atributos tendrán nuestras tablas, las llaves foráneas
         * y si queremos que seán valores únicos para evitar duplicados, por ejemplo, en el caso de los proveedores
         * podemos elegir el número de nit como valor único, de esa manera evitaremos duplicidad de registros, ya que
         * un proveedor solo puede tener un número de nit */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS"); //formatos unicode

            //atributos especiales para la tabla productos
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(p => new { p.CodigoProducto }) //elegimos CodigoProducto como atributo no duplicado
                .IsUnique(true);

                //relacion movimientos-productos
                entity.HasOne(d => d.IdMovimientoNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMovimiento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_IdMovimiento_Producto");
            });

            //atributos especiales para la tabla proveedores
            modelBuilder.Entity<Proveedor>(entity =>
            { 
                entity.HasIndex(p => new { p.NitProveedor }) //elegimos CodigoMovimiento como atributo no duplicado
                .IsUnique(true);

                //relacion movimientos-proveedores
                entity.HasOne(d => d.IdMovimientoNavigation)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdMovimiento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_IdMovimiento_Proveedor");
            });

            //atributos especiales para la tabla movimientos
            modelBuilder.Entity<Movimiento>(entity => {
                entity.HasIndex(p => new { p.CodigoMovimiento }) //elegimos CodigoMovimiento como atributo no duplicado
                .IsUnique(true);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
