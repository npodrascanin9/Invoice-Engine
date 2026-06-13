using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class ProductEntityConfiguration :
    IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(250);

        builder.Property(x => x.UnitOfMeasureCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.HasMany(x => x.Articles)
            .WithOne(article => article.Product)
            .HasForeignKey(article => article.ProductId)
            .HasConstraintName("FK_Articles_ProductId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
