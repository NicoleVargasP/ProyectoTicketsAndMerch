using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Data;

public partial class TicketsAndMerchContext : DbContext
{
    public TicketsAndMerchContext()
    {
    }

    public TicketsAndMerchContext(DbContextOptions<TicketsAndMerchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Concert> Concerts { get; set; }

    public virtual DbSet<Merch> Merches { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<BuyTicket> BuyTickets { get; set; }

    /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
          => optionsBuilder.UseSqlServer("Server=LAPTOP-7H3CIR36;Database=DbTicketsAndMerch;Trusted_Connection=True;TrustServerCertificate=True;");
      */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
        /*modelBuilder.Entity<Concert>(entity =>
        {
            entity.HasKey(e => e.ConcertId).HasName("PK__Concerts__06ED37AC1A7B74F7");

            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Merch>(entity =>
        {
            entity.HasKey(e => e.MerchId).HasName("PK__Merch__A1D5A9EE9E356FFA");

            entity.ToTable("Merch");

            entity.Property(e => e.MerchName).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TypeMerch).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF95AFB071");

            entity.Property(e => e.DateOrder)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Merch).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MerchId)
                .HasConstraintName("FK__Orders__MerchId__59063A47");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Orders__TicketId__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserId__571DF1D5");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38918D5B09");

            entity.Property(e => e.Method).HasMaxLength(100);
            entity.Property(e => e.OrderAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentState).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__OrderI__5CD6CB2B");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC6071B96D81E");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TicketType).HasMaxLength(100);

            entity.HasOne(d => d.Concert).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ConcertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__Concert__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C56DDB4AD");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348DBAE2A7").IsUnique();

            entity.Property(e => e.Contrasenia).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
*/