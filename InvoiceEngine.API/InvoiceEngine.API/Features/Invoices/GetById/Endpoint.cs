namespace InvoiceEngine.API.Features.Invoices.GetById;

public class GetInvoiceByIdEndpoint :
    ICarterModule
{
    public void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app.MapGet("api/invoices/{id}", async (
            ISender sender,
            int id) =>
            {
                var query = new GetInvoiceByIdQuery(id);

                var result = await sender.Send(query);

                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("GetInvoiceById")
            .WithDescription("Get Invoice by Id")
            .WithOpenApi();
    }
}
