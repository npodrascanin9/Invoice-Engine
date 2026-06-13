using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemTypeEntityConfiguration : IEntityTypeConfiguration<InvoiceItemType>
{
    public void Configure(EntityTypeBuilder<InvoiceItemType> builder)
    {
        builder.ToTable("InvoiceItemTypes");

        builder.HasKey(x => x.Id);

        // Id is not identity
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName("UX_InvoiceItemTypes_Name");
    }
}
