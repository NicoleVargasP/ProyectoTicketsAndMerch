using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
               .ValueGeneratedOnAdd();

        builder.Property(o => o.DateOrder)
               .HasColumnType("datetime");

        builder.Property(o => o.State)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);

        builder.Property(o => o.OrderDetail)
               .HasMaxLength(500)
               .IsUnicode(false);

        builder.Property(o => o.OrderAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(o => o.UnitPrice)
               .HasColumnType("decimal(18,2)");

        // User
        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        // Merch ✅
        builder.HasOne(o => o.Merch)
               .WithMany()
               .HasForeignKey(o => o.MerchId)
               .OnDelete(DeleteBehavior.SetNull);

        // Ticket ✅
        builder.HasOne(o => o.Ticket)
               .WithMany()
               .HasForeignKey(o => o.TicketId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
