using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class OrderEntityConfiguration :
    IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.SellDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.CreatedAt)
           .HasColumnType("DATETIME")
           .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(x => x.Items)
            .WithOne(item => item.Order)
            .HasForeignKey(item => item.OrderId)
            .HasConstraintName("FK_OrderItems_OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.InvoiceItemOrderDetails)
            .WithOne(detail => detail.Order)
            .HasForeignKey(detail => detail.OrderId)
            .HasConstraintName("FK_InvoiceItemOrderDetails_OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
