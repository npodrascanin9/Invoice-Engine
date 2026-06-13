namespace InvoiceEngine.API.Features.Invoices.Shared;

public class TransportInvoiceItemRequest
{
    public int TransportCompanyId { get; set; }
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }
    public decimal Price { get; set; }
}
