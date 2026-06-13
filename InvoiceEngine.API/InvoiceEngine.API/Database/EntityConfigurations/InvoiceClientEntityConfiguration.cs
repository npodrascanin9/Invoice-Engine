using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceClientEntityConfiguration :
    IEntityTypeConfiguration<InvoiceClient>
{
    public void Configure(
        EntityTypeBuilder<InvoiceClient> builder)
    {
        builder.HasKey(x => new
        {
            x.InvoiceId,
            x.ClientId,
            x.SubjectCode
        });

        builder.Property(x => x.SubjectCode)
            .HasConversion<int>()
            .IsRequired();
    }
}
