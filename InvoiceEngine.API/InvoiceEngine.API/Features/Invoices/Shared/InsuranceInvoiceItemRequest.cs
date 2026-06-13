namespace InvoiceEngine.API.Features.Invoices.Shared;

public class InsuranceInvoiceItemRequest
{
    public int InsuranceCompanyId { get; set; }
    public DateOnly ExpiresAt { get; set; }
    public decimal Price { get; set; }
}
