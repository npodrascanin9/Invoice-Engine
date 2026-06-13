using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class ArticleEntityConfiguration :
    IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExpiresAt)
            .IsRequired(false)
            .HasColumnType("Date");

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(15)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(x => x.OrderItems)
            .WithOne(orderItem => orderItem.Article)
            .HasForeignKey(orderItem => orderItem.ArticleId)
            .HasConstraintName("FK_OrderItems_ArticleId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
