using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceCustomIncotermObligationEntityConfiguration :
    IEntityTypeConfiguration<InvoiceCustomIncotermObligation>
{
    public void Configure(
        EntityTypeBuilder<InvoiceCustomIncotermObligation> builder)
    {
        builder.ToTable("CustomIncotermObligations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.FromSubjectCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ToSubjectCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ItemTypeCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.PercentageAmount)
            .HasColumnType("DECIMAL(18, 2)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");


        builder.HasCheckConstraint(
            "CK_CustomIncotermObligations_PercentageAmount",
            "PercentageAmount >= 0 AND PercentageAmount <= 100");

        builder.HasIndex(x => new
            {
                x.InvoiceId,
                x.FromSubjectCode,
                x.ToSubjectCode,
                x.ItemTypeCode
            })
            .HasDatabaseName("UX_CustomIncotermObligations_Invoice_SubjectFrom_SubjectTo_ItemType")
            .IsUnique();
    }
}
