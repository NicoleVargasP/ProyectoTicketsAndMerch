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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Users");

            // Clave primaria
            builder.HasKey(e => e.Id)
                   .HasName("PK_Users");

            // ID autogenerado
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // Nombre de usuario obligatorio (máx 100)
            builder.Property(e => e.UserName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Correo obligatorio (máx 150)
            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(150)
                   .IsUnicode(false);

            // Contraseña obligatoria (máx 100)
            builder.Property(e => e.Contrasenia)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Rol (Administrador, Cliente, etc.)
            builder.Property(e => e.Rol)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Relación 1:N con Orders
            builder.HasMany(d => d.Orders)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Orders_Users");
        }
    }
}
