using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class ClientEntityConfiguration :
    IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IdentificationNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.HasIndex(x => x.IdentificationNumber)
            .IsUnique()
            .HasDatabaseName("UX_Clients_IdentificationNumber");

        builder.Property(x => x.Email)
            .HasMaxLength(180)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.HasIndex(x => x.IdentificationNumber)
            .IsUnique()
            .HasDatabaseName("UX_Clients_IdentificationNumber")
            .HasFilter("[IdentificationNumber] IS NOT NULL");

        builder.HasMany(x => x.InvoiceClients)
            .WithOne(invoiceClient => invoiceClient.Client)
            .HasForeignKey(invoiceClient => invoiceClient.ClientId)
            .HasConstraintName("FK_InvoiceClients_ClientId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
