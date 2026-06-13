using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemObligationEntityConfiguration :
    IEntityTypeConfiguration<InvoiceItemObligation>
{
    public void Configure(EntityTypeBuilder<InvoiceItemObligation> builder)
    {
        builder.ToTable("InvoiceItemObligations");

        builder.HasKey(x => new
        {
            x.InvoiceItemId,
            x.FromClientSubjectCode,
            x.ToClientSubjectCode
        });
        
        builder.Property(x => x.FromClientSubjectCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ToClientSubjectCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.OwingAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.HasCheckConstraint(
            name: "CK_Obligation_DifferentClients",
            sql: "[FromClientSubjectCode] <> [ToClientSubjectCode]");
    }
}
