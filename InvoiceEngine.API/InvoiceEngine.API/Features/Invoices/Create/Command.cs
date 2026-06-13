namespace InvoiceEngine.API.Features.Invoices.Create;

public record CreateInvoiceCommand(
    IncotermRule Incoterm,
    DateOnly IssuedAt,
    DateOnly ExpiresAt,
    DateOnly TransactionStartDate,
    DateOnly TransactionEndDate,
    int ClientSellerId,
    int ClientBuyerId,
    SellGoodsInvoiceItemRequest? SellGoodsInvoiceItemRequest,
    TransportInvoiceItemRequest? TransportInvoiceItemRequest,
    InsuranceInvoiceItemRequest? InsuranceInvoiceItemRequest)
    : ICommand<Result<CreateInvoiceResponse>>;
