namespace InvoiceEngine.API.Features.Invoices.GetById;

internal sealed class GetInvoiceByIdQueryHandler(
    ApplicationDbContext context) :
    IQueryHandler<GetInvoiceByIdQuery, Result<GetInvoiceByIdResponse>>
{
    public async Task<Result<GetInvoiceByIdResponse>> Handle(
        GetInvoiceByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var invoice = await context
            .Invoices
            .AsNoTracking()
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

        return Result.Success(query.ToResponse(invoice, clients));
    }
}
