using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemEntityConfiguration : 
    IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItems");

        builder.HasKey(x => x.Id);

        // ItemTypeCode (enum => int)
        builder.Property(x => x.ItemTypeCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(250);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.HasMany(x => x.ItemObligations)
            .WithOne(obligation => obligation.InvoiceItem)
            .HasForeignKey(obligation => obligation.InvoiceItemId)
            .HasConstraintName("FK_InvoiceItemObligations_InvoiceItemId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
