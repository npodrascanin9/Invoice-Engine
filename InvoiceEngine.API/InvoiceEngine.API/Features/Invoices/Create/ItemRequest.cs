namespace InvoiceEngine.API.Features.Invoices.Create;

public record CreateInvoiceItemRequest(
    string? Description,
    decimal Amount);
