using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemTransportDetailEntityConfiguration :
    IEntityTypeConfiguration<InvoiceItemTransportDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceItemTransportDetail> builder)
    {
        builder.ToTable("InvoiceItemTransportDetails");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceItemId)
            .IsRequired();

        builder.Property(x => x.TransportCompanyId)
            .IsRequired(false);

        builder.Property(x => x.AddressFrom)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.AddressTo)
            .IsRequired()
            .HasMaxLength(150);
    }
}
