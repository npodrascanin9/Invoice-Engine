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

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        /*
        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)")
            .HasComputedColumnSql(
                "[Quantity] * [Price]", 
                stored: true);
        */

        builder.HasMany(x => x.ItemOrderDetails)
            .WithOne(itemOrderDetail => itemOrderDetail.InvoiceItem)
            .HasForeignKey(itemOrderDetail => itemOrderDetail.InvoiceItemId)
            .HasConstraintName("FK_InvoiceItemOrderDetails_InvoiceItemId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ItemTransportDetails)
            .WithOne(itemTransportDetail => itemTransportDetail.InvoiceItem)
            .HasForeignKey(itemTransportDetail => itemTransportDetail.InvoiceItemId)
            .HasConstraintName("FK_InvoiceItemTransportDetails_InvoiceItemId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ItemInsuranceDetails)
            .WithOne(itemInsuranceDetail => itemInsuranceDetail.InvoiceItem)
            .HasForeignKey(itemInsuranceDetail => itemInsuranceDetail.InvoiceItemId)
            .HasConstraintName("FK_InvoiceItemInsuranceDetails_InvoiceItemId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ItemObligations)
            .WithOne(obligation => obligation.InvoiceItem)
            .HasForeignKey(obligation => obligation.InvoiceItemId)
            .HasConstraintName("FK_InvoiceItemObligations_InvoiceItemId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
