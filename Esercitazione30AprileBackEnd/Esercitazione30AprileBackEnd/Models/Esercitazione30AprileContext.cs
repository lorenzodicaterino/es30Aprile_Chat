using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Esercitazione30AprileBackEnd.Models;

public partial class Esercitazione30AprileContext : DbContext
{
    public Esercitazione30AprileContext()
    {
    }

    public Esercitazione30AprileContext(DbContextOptions<Esercitazione30AprileContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-11\\SQLEXPRESS;Database=Esercitazione30Aprile;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.UtenteId).HasName("PK__utente__CA5C2253956D3038");

            entity.ToTable("utente");

            entity.HasIndex(e => e.Username, "UQ__utente__F3DBC57275D2C21E").IsUnique();

            entity.Property(e => e.UtenteId).HasColumnName("utenteID");
            entity.Property(e => e.CodiceUtente)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codice_utente");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("deleted");
            entity.Property(e => e.Passw)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("passw");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
