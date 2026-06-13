using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class OrderItemEntityConfiguration :
    IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.ArticleId)
            .IsRequired();
        
        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasColumnType("decimal(18, 2)")
            .HasComputedColumnSql(
                "[Quantity] * [Price]",
                stored: true);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");
    }
}
