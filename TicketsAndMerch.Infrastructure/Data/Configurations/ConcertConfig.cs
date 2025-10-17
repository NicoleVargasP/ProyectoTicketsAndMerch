using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Data.Configurations
{
    public class ConcertConfig : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Concerts");

            // Clave primaria
            builder.HasKey(e => e.ConcertId)
                   .HasName("PK_Concerts");

            
            builder.Property(e => e.ConcertId)
                   .ValueGeneratedNever();

            // Campo Title obligatorio, hasta 100 caracteres
            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Campo Description opcional, hasta 500 caracteres
            builder.Property(e => e.Description)
                   .HasMaxLength(500)
                   .IsUnicode(false);

            // Campo Location obligatorio, hasta 100 caracteres
            builder.Property(e => e.Location)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Tipo de dato para la fecha
            builder.Property(e => e.Date)
                   .HasColumnType("date");

            // Número de entradas disponibles
            builder.Property(e => e.AvailableTickets)
                   .HasColumnName("AvailableTickets");

            // Relación 1 (Concert) a N (Tickets)
            builder.HasMany(d => d.Tickets)
                   .WithOne(p => p.Concert)
                   .HasForeignKey(p => p.ConcertId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Tickets_Concerts");
        }
    }
}