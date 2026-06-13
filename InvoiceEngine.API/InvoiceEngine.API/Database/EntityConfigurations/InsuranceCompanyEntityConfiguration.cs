using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InsuranceCompanyEntityConfiguration :
    IEntityTypeConfiguration<InsuranceCompany>
{
    public void Configure(
        EntityTypeBuilder<InsuranceCompany> builder)
    {
        builder.ToTable("InsuranceCompanies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IdentificationNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.HasIndex(x => x.IdentificationNumber)
            .IsUnique()
            .HasDatabaseName("UX_InsuranceCompanies_IdentificationNumber")
            .HasFilter("IdentificationNumber IS NOT NULL");

        builder.HasMany(x => x.InvoiceItemInsuranceDetails)
            .WithOne(detail => detail.InsuranceCompany)
            .HasForeignKey(detail => detail.InsuranceCompanyId)
            .HasConstraintName("FK_InvoiceItemInsuranceDetails_InsuranceCompanyId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
