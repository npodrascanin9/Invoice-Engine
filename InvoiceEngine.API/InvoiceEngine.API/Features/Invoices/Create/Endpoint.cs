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
                var command = request.ToCommand();

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
