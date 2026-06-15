namespace InvoiceEngine.API.Features.Invoices.Create;

public record CreateInvoiceCommand(
    IncotermRule Incoterm,
    DateOnly IssuedAt,
    DateOnly ExpiresAt,
    DateOnly TransactionStartDate,
    DateOnly TransactionEndDate,
    int ClientSellerId,
    int ClientBuyerId,
    Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest> Items)
    : ICommand<Result<CreateInvoiceResponse>>;
