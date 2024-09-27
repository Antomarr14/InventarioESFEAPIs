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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();  
        }
    }
}
