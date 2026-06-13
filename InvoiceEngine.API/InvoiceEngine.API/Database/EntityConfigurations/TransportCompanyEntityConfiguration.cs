using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class TransportCompanyEntityConfiguration :
    IEntityTypeConfiguration<TransportCompany>
{
    public void Configure(
        EntityTypeBuilder<TransportCompany> builder)
    {
        builder.ToTable("TransportCompanies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IdentificationNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.HasIndex(x => x.IdentificationNumber)
            .IsUnique()
            .HasDatabaseName("UX_TransportCompanies_IdentificationNumber")
            .HasFilter("IdentificationNumber IS NOT NULL");

        builder.HasMany(x => x.InvoiceItemTransportDetails)
           .WithOne(d => d.TransportCompany)
           .HasForeignKey(d => d.TransportCompanyId)
           .HasConstraintName("FK_InvoiceItemTransportDetails_TransportCompanyId")
           .OnDelete(DeleteBehavior.Cascade);
    }
}
