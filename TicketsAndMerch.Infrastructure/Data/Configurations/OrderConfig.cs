using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Orders");

            // Clave primaria
            builder.HasKey(e => e.Id)
                   .HasName("PK_Orders");

            // Generar automáticamente el ID
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // Fecha del pedido
            builder.Property(e => e.DateOrder)
                   .HasColumnType("datetime");

            // Estado (pendiente, pagado, enviado, etc.)
            builder.Property(e => e.State)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Detalle opcional del pedido
            builder.Property(e => e.OrderDetail)
                   .HasMaxLength(500)
                   .IsUnicode(false);

            // Cantidad de artículos
            builder.Property(e => e.OrderAmount)
                   .IsRequired();

            // Precio unitario
            builder.Property(e => e.UnitPrice)
                   .HasColumnType("decimal(10,2)");

            // Relación con User (1 usuario -> muchos pedidos)
            builder.HasOne(d => d.User)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Orders_Users");

            // Relación con Merch (opcional)
            builder.HasOne(d => d.Merch)
                   .WithMany()
                   .HasForeignKey(d => d.MerchId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Orders_Merch");

            // Relación con Ticket (opcional)
            builder.HasOne(d => d.Ticket)
                   .WithMany()
                   .HasForeignKey(d => d.TicketId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Orders_Tickets");
        }
    }
}