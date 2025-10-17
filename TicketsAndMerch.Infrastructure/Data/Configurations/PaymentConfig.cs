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
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Payments");

            // Clave primaria
            builder.HasKey(e => e.PaymentId)
                   .HasName("PK_Payments");

            // PaymentId autogenerado
            builder.Property(e => e.PaymentId)
                   .ValueGeneratedOnAdd();

            // Método de pago (tarjeta, efectivo, QR, etc.)
            builder.Property(e => e.Method)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Fecha de pago
            builder.Property(e => e.PaymentDate)
                   .HasColumnType("datetime");

            // Monto del pedido pagado
            builder.Property(e => e.OrderAmount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            // Estado del pago (Pendiente, Pagado, Cancelado)
            builder.Property(e => e.PaymentState)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Relación con Order (si existe)
            builder.HasOne(d => d.Order)
                   .WithMany()
                   .HasForeignKey(d => d.OrderId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Payments_Orders");
        }
    }
}
