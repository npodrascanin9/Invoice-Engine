namespace InvoiceEngine.API.Features.Invoices.Create;

public record CreateInvoiceRequest(
    IncotermRule Incoterm,
    DateOnly IssuedAt,
    DateOnly ExpiresAt,
    DateOnly TransactionStartDate,
    DateOnly TransactionEndDate,
    int ClientSellerId,
    int ClientBuyerId,
    // Dictionary<InvoiceItemTypeCode, object>  Items,
    SellGoodsInvoiceItemRequest? SellGoodsInvoiceItemRequest,
    TransportInvoiceItemRequest? TransportInvoiceItemRequest,
    InsuranceInvoiceItemRequest? InsuranceInvoiceItemRequest);
