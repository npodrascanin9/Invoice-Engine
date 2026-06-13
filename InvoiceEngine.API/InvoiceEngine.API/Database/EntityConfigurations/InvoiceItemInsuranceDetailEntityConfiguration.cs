using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemInsuranceDetailEntityConfiguration :
    IEntityTypeConfiguration<InvoiceItemInsuranceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceItemInsuranceDetail> builder)
    {
        builder.ToTable("InvoiceItemInsuranceDetails");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceItemId)
            .IsRequired();

        builder.Property(x => x.InsuranceCompanyId)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnType("date")
            .IsRequired();
    }
}
