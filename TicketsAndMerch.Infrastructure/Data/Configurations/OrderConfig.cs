using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Tabla
            builder.ToTable("Orders");

            // Clave primaria
            builder.HasKey(e => e.Id)
                   .HasName("PK_Orders");

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // Fecha del pedido
            builder.Property(e => e.DateOrder)
                   .HasColumnType("datetime");

            // Estado
            builder.Property(e => e.State)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Detalle
            builder.Property(e => e.OrderDetail)
                   .HasMaxLength(500)
                   .IsUnicode(false);

            // Cantidad de artículos y precio
            builder.Property(e => e.OrderAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            // Relación con User
            builder.HasOne(d => d.User)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Orders_Users");

            // Relación con Merch
            builder.HasOne(d => d.Merch)
                   .WithMany()
                   .HasForeignKey(d => d.MerchId)
                   .HasPrincipalKey(m => m.Id)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Orders_Merch");

            // Relación con Ticket
            builder.HasOne(d => d.Ticket)
                   .WithMany()
                   .HasForeignKey(d => d.TicketId)
                   .HasPrincipalKey(t => t.Id)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Orders_Tickets");

        }
    }
}
