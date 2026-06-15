namespace InvoiceEngine.API.Database.Entities;

public class Invoice
{
    public int Id { get; set; }
    public IncotermRule IncotermCode { get; set; }
    public InvoiceStatus StatusCode { get; set; }
    public DateOnly IssuedAt { get; set; }
    public DateOnly ExpiresAt { get; set; }
    public DateOnly TransactionStartDate { get; set; }
    public DateOnly TransactionEndDate { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public List<InvoiceItem> Items { get; set; } = new();
    public List<InvoiceClient> InvoiceClients { get; set; } = new();
    public List<InvoiceCustomIncotermObligation> CustomIncotermObligations { get; set; } = new();
}
