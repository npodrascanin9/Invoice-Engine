namespace InvoiceEngine.API.Features.Invoices.Create;

public class CreateInvoiceEndpoint :
    ICarterModule
{
    public void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app.MapPost("api/invoices", async (
            ISender sender,
            CreateInvoiceRequest request) =>
            {
                var command = new CreateInvoiceCommand(
                    request.Incoterm,
                    request.IssuedAt,
                    request.ExpiresAt,
                    request.TransactionStartDate,
                    request.TransactionEndDate,
                    request.ClientBuyerId,
                    request.ClientSellerId,
                    request.SellGoodsInvoiceItemRequest,
                    request.TransportInvoiceItemRequest,
                    request.InsuranceInvoiceItemRequest);

                var result = await sender.Send(command);

                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("CreateInvoice")
            .WithDescription("Create invoice")
            .WithOpenApi();
    }
}
