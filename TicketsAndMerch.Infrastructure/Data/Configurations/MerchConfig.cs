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
    public class MerchConfig : IEntityTypeConfiguration<Merch>
    {
        public void Configure(EntityTypeBuilder<Merch> builder)
        {
            // Nombre de la tabla en SQL
            builder.ToTable("Merch");

            // Clave primaria
            builder.HasKey(e => e.Id)
                   .HasName("PK_Merch");

            // MerchId generado automáticamente
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // MerchName obligatorio, hasta 100 caracteres
            builder.Property(e => e.MerchName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Descripción opcional, hasta 500 caracteres
            builder.Property(e => e.Description)
                   .HasMaxLength(500)
                   .IsUnicode(false);

            // Precio con precisión decimal (10,2)
            builder.Property(e => e.Price)
                   .HasColumnType("decimal(10, 2)");

            // Tipo de merch (camiseta, poster, etc.)
            builder.Property(e => e.TypeMerch)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Stock (entero, obligatorio)
            builder.Property(e => e.Stock)
                   .IsRequired();
        }
    }
}
