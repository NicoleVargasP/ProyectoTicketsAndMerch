using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsAndMerch.Core.CustomEntities;

namespace TicketsAndMerch.Infrastructure.Data.Configurations
{
    public class BuyTicketConfig : IEntityTypeConfiguration<BuyTicket>
    {
        public void Configure(EntityTypeBuilder<BuyTicket> builder)
        {
            builder.ToTable("BuyTickets");

            builder.HasKey(bt => bt.Id)
                   .HasName("PK_BuyTickets");

            builder.Property(bt => bt.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(bt => bt.UserId)
                   .IsRequired();

            builder.Property(bt => bt.ConcertId)
                   .IsRequired();

            builder.Property(bt => bt.Quantity)
                   .IsRequired();

            builder.Property(bt => bt.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(bt => bt.PurchaseDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(bt => bt.PaymentState)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
