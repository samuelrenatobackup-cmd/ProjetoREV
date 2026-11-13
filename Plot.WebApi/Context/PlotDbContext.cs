using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Plot.WebApi.Models;

namespace Plot.WebApi.Context;

public partial class PlotDbContext : DbContext
{
    public PlotDbContext()
    {
    }

    public PlotDbContext(DbContextOptions<PlotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filme> Filme { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D16S20-1252895;Database=PlotDB;User Id=sa;Password=Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PK__Filme__6E8F2A76AF3ED431");

            entity.HasMany(d => d.IdGenero).WithMany(p => p.IdFilme)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmeGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GeneroFilmeGenero"),
                    l => l.HasOne<Filme>().WithMany()
                        .HasForeignKey("IdFilme")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FilmeFilmeGenero"),
                    j =>
                    {
                        j.HasKey("IdFilme", "IdGenero");
                    });
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F8349884797D4E3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97DC317583");

            entity.ToTable(tb => tb.HasTrigger("TR_UsuarioSoftDelete"));

            entity.Property(e => e.Ativo).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
