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
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Tickets");

            // Clave primaria
            builder.HasKey(e => e.TicketId)
                   .HasName("PK_Tickets");

            // TicketId generado automáticamente
            builder.Property(e => e.TicketId)
                   .ValueGeneratedOnAdd();

            // Precio con dos decimales
            builder.Property(e => e.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            // Tipo de ticket (ej: VIP, General, Preferencial)
            builder.Property(e => e.TicketType)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Stock disponible
            builder.Property(e => e.Stock)
                   .IsRequired();

            // Relación con Concert
            builder.HasOne(d => d.Concert)
                   .WithMany(p => p.Tickets)
                   .HasForeignKey(d => d.ConcertId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Tickets_Concerts");
        }
    }
}
