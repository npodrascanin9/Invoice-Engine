namespace InvoiceEngine.API.Features.Invoices.GetById;

public record GetInvoiceByIdResponse(
    int Id,
    IncotermRule IncotermRuleCode,
    string IncotermRule,
    InvoiceStatus StatusCode,
    string Status,
    DateOnly IssuedAt,
    DateOnly ExpiresAt,
    DateOnly TransactionStartDate,
    DateOnly TransactionEndDate,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Dictionary<InvoiceSubject, ClientInvoiceDetailsResponse> Clients);

public record ClientInvoiceDetailsResponse(
    int ClientId,
    string Name,
    List<InvoiceDetailItemResponse> Items);

public record InvoiceDetailItemResponse(
    int Id,
    InvoiceItemTypeCode ItemTypeCode,
    string ItemType,
    string PaysTo,
    decimal Amount);
