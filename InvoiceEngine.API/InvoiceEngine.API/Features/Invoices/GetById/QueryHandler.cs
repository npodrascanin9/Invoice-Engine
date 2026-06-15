namespace InvoiceEngine.API.Features.Invoices.GetById;

internal sealed class QueryHandler(
    ApplicationDbContext context) :
    IQueryHandler<GetInvoiceByIdQuery, Result<GetInvoiceByIdResponse>>
{
    public async Task<Result<GetInvoiceByIdResponse>> Handle(
        GetInvoiceByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var invoice = await context
            .Invoices
            .Include(x => x.InvoiceClients)
            .ThenInclude(invoiceClient => invoiceClient.Client)
            .Include(x => x.Items)
            .ThenInclude(item => item.ItemObligations)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (invoice is null)
        {
            return Result.Failure<GetInvoiceByIdResponse>(
                InvoiceErrors.NotFound(query.Id));
        }

        var clients = invoice.InvoiceClients
            .ToDictionary(
                invoiceClient => invoiceClient.SubjectCode,
                invoiceClient => new ClientInvoiceDetailsResponse(
                    ClientId: invoiceClient.ClientId,
                    Name: invoiceClient.Client.Name,
                    Items: query.MapItemsForClient(invoiceClient.SubjectCode, invoice.Items)));

        var response = new GetInvoiceByIdResponse(
            Id: invoice.Id,
            IncotermRuleCode: invoice.IncotermCode,
            IncotermRule: invoice.IncotermCode.ToString(),
            StatusCode: invoice.StatusCode,
            Status: invoice.StatusCode.ToString(),
            IssuedAt: invoice.IssuedAt,
            ExpiresAt: invoice.ExpiresAt,
            TransactionStartDate: invoice.TransactionStartDate,
            TransactionEndDate: invoice.TransactionEndDate,
            CreatedAt: invoice.CreatedAt,
            UpdatedAt: invoice.UpdatedAt,
            Clients: clients
        );

        return Result.Success(response);
    }
}
