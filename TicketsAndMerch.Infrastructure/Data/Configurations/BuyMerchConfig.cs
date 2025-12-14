// TicketsAndMerch.Infrastructure.Data.Configurations/BuyMerchConfig.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsAndMerch.Core.CustomEntities;

namespace TicketsAndMerch.Infrastructure.Data.Configurations
{
    public class BuyMerchConfig : IEntityTypeConfiguration<BuyMerch>
    {
        public void Configure(EntityTypeBuilder<BuyMerch> builder)
        {
            builder.ToTable("BuyMerchs");

            builder.HasKey(bm => bm.Id)
                   .HasName("PK_BuyMerchs");

            builder.Property(bm => bm.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(bm => bm.UserId)
                   .IsRequired();

            builder.Property(bm => bm.MerchId)
                   .IsRequired();

            builder.Property(bm => bm.Quantity)
                   .IsRequired();

            builder.Property(bm => bm.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(bm => bm.PurchaseDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(bm => bm.PaymentState)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(bm => bm.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
