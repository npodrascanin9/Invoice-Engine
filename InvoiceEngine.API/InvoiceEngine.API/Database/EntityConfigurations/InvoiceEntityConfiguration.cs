using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceEntityConfiguration :
    IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusCode)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.IncotermCode)
            .HasConversion<string>()
            .HasColumnType("VARCHAR(3)")
            .IsRequired();
        
        builder.Property(x => x.IssuedAt)
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(x => x.TransactionStartDate)
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(x => x.TransactionEndDate)
            .HasColumnType("DATE")
            .IsRequired();

        
        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

        
        builder.HasCheckConstraint(
            name: "CK_Invoice_TransactionDateRange",
            sql: "[TransactionEndDate] > [TransactionStartDate]");

        builder.HasCheckConstraint(
            name: "CK_Invoice_IssueExpireRange",
            sql: "[ExpiresAt] > [IssuedAt]");

        builder.HasMany(x => x.Items)
            .WithOne(item => item.Invoice)
            .HasForeignKey(item => item.InvoiceId)
            .HasConstraintName("FK_InvoiceItems_InvoiceId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.InvoiceClients)
            .WithOne(invoiceClient => invoiceClient.Invoice)
            .HasForeignKey(invoiceClient => invoiceClient.InvoiceId)
            .HasConstraintName("FK_InvoiceClients_InvoiceId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CustomIncotermObligations)
            .WithOne(customObligation => customObligation.Invoice)
            .HasForeignKey(customObligation => customObligation.InvoiceId)
            .HasConstraintName("FK_InvoiceCustomIncotermObligations_InvoiceId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
