namespace InvoiceEngine.API.Features.Invoices.Shared;

public static class InvoiceErrors
{
    public static Error ClientBuyerNotFound(
        int id)
    {
        return new(
            Code: "Invoices.NotFound",
            Description: $"Client buyer with Id='{id}' not found");
    }

    public static Error ClientSellerNotFound(
        int id)
    {
        return new(
            Code: "Invoices.NotFound",
            Description: $"Client seller with Id='{id}' not found");
    }
}
