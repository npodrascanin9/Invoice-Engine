namespace InvoiceEngine.API.Database.Entities;

public class TransportCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? IdentificationNumber { get; set; }

    public List<InvoiceItemTransportDetail> InvoiceItemTransportDetails { get; set; } = new();
}
