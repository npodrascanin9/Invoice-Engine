namespace InvoiceEngine.API.Database.Entities;

public class InvoiceItemOrderDetail
{
    public int Id { get; set; }
    public int InvoiceItemId { get; set; }
    public int OrderId { get; set; }

    public InvoiceItem InvoiceItem { get; set; }
    public Order Order { get; set; }
}
