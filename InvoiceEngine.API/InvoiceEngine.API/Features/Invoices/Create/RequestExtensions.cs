namespace InvoiceEngine.API.Features.Invoices.Create;

public static class CreateInvoiceRequestExtensions
{
    public static CreateInvoiceCommand ToCommand(
        this CreateInvoiceRequest request)
    {
        return new CreateInvoiceCommand(
            request.Incoterm,
            IssuedAt: request.IssuedAt,
            ExpiresAt: request.ExpiresAt,
            TransactionStartDate: request.TransactionStartDate,
            TransactionEndDate: request.TransactionEndDate,
            ClientSellerId: request.ClientSellerId,
            ClientBuyerId: request.ClientBuyerId,
            request.Items,
            request.CustomIncotermRules);
    }
}
