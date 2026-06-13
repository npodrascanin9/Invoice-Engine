using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceEngine.API.Database.EntityConfigurations;

public class InvoiceItemOrderDetailEntityConfiguration :
    IEntityTypeConfiguration<InvoiceItemOrderDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceItemOrderDetail> builder)
    {
        builder.ToTable("InvoiceItemOrderDetails");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceItemId)
            .IsRequired();

        builder.Property(x => x.OrderId)
            .IsRequired();
    }
}
