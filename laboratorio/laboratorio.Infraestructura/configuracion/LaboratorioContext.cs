using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using laboratorio.modelos;

namespace laboratorio.Infraestructura;

public partial class LaboratorioContext : DbContext
{
    public LaboratorioContext()
    {
    }

    public LaboratorioContext(DbContextOptions<LaboratorioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=laboratorio;Username=postgres;Password=20yuver;");
    }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("producto_id_proveedor_fkey");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("proveedor_pkey");

            entity.ToTable("proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
