using System;
using InventarioESFEAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Context
{
    public class InventarioESFEContext : DbContext
    {
        public InventarioESFEContext(DbContextOptions<InventarioESFEContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Proveedor> Proveedor { get; set; } 
        public DbSet<Usuario> Usuario { get; set; } 
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Control> Control { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<Perdidas> Perdidas { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<RolControl> RolControl { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<AsignacionCodigo> AsignacionCodigo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();  
                base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Imagen>()
                .HasOne(i => i.AsignacionCodigo)
                .WithMany(a => a.Imagenes)
                .HasForeignKey(i => i.IdAsignacionCodigo)
                .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Marca)
    .WithMany()
    .HasForeignKey(a => a.IdMarca)
    .HasConstraintName("FK_Articulo_Marca"); // Aquí podrías usar el nombre correcto si es necesario.

modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Categoria)
    .WithMany()
    .HasForeignKey(a => a.IdCategoria)
    .HasConstraintName("FK_Articulo_Categoria");

    modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Ubicacion)
    .WithMany()
    .HasForeignKey(a => a.IdUbicacion)
    .HasConstraintName("FK_Articulo_Ubicacion");

modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Usuario)
    .WithMany(a => a.Articulos)
    .HasForeignKey(a => a.IdUsuario)
    .HasConstraintName("FK_Articulo_Usuario");

modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Estado)
    .WithMany(a => a.Articulos)
    .HasForeignKey(a => a.IdEstado)
    .HasConstraintName("FK_Articulo_Estado");

modelBuilder.Entity<Articulo>()
    .HasOne(a => a.Tipo)
    .WithMany()
    .HasForeignKey(a => a.IdTipo)
    .HasConstraintName("FK_Articulo_Tipo");

            // Configuración de Compra
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany()
                .HasForeignKey(c => c.IdProveedor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.IdEstado)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de DetalleCompra
            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.articulo)
                .WithMany(p => p.DetalleCompras)
                .HasForeignKey(dc => dc.IdArticulo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Compra)
                .WithMany(c => c.Detalles)
                .HasForeignKey(dc => dc.IdCompra)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
