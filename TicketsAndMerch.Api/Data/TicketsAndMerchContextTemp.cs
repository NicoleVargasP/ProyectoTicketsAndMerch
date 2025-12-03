using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketsAndMerch.Api.Data;

public partial class TicketsAndMerchContextTemp : DbContext
{
    public TicketsAndMerchContextTemp()
    {
    }

    public TicketsAndMerchContextTemp(DbContextOptions<TicketsAndMerchContextTemp> options)
        : base(options)
    {
    }

    public virtual DbSet<Security> Securities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7H3CIR36;Database=DbTicketsAndMerch;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Security>(entity =>
        {
            entity.ToTable("Security");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
