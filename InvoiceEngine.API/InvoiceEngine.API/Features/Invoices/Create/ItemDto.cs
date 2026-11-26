namespace InvoiceEngine.API.Features.Invoices.Create;

public record CreateInvoiceItemDto(
    string? Description,
    decimal Amount);
