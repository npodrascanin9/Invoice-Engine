namespace InvoiceEngine.API.Features.Invoices.Create;

internal sealed class CreateInvoiceCommandHandler(
    ApplicationDbContext context,
    IIncotermRuleObligationStrategyContext obligationStrategyContext) :
    ICommandHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
{
    public async Task<Result<CreateInvoiceResponse>> Handle(
        CreateInvoiceCommand command, 
        CancellationToken cancellationToken)
    {
        if (!await context.Clients.AnyAsync(
            x => x.Id == command.ClientSellerId,
            cancellationToken))
        {
            return Result.Failure<CreateInvoiceResponse>(
                InvoiceErrors.ClientSellerNotFound(command.ClientSellerId));
        }

        if (!await context.Clients.AnyAsync(
            x => x.Id == command.ClientBuyerId,
            cancellationToken))
        {
            return Result.Failure<CreateInvoiceResponse>(
                InvoiceErrors.ClientBuyerNotFound(command.ClientBuyerId));
        }

        Invoice invoiceEntity = command.ToInvoice();
        
        InitializeCustomObligationsIfHasPermission(
            command, 
            invoiceEntity);

        invoiceEntity
            .InvoiceClients
            .AddRange(command.ToInvoiceClients());

        invoiceEntity
            .Items
            .AddRange(command.ToInvoiceItems());

        InitializeItemObligations(
            command,
            invoiceEntity);

        await context
            .Invoices
            .AddAsync(invoiceEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateInvoiceResponse>(
            new(invoiceEntity.Id));
    }

    private static void InitializeCustomObligationsIfHasPermission(
        CreateInvoiceCommand command, 
        Invoice invoiceEntity)
    {
        if (command.CanHaveCustomIncotermObligations())
        {
            invoiceEntity.CustomIncotermObligations.AddRange(
                command.ToCustomIncotermObligations());
        }
    }

    private void InitializeItemObligations(
        CreateInvoiceCommand command, 
        Invoice invoiceEntity)
    {
        foreach (var invoiceItem in invoiceEntity.Items)
            InitializeItemObligation(command, invoiceItem);
    }

    private void InitializeItemObligation(
        CreateInvoiceCommand command, 
        InvoiceItem invoiceItem)
    {
        var incotermPairs = obligationStrategyContext.ResolveIncotermObligation(
            command.Incoterm,
            invoiceItem.ItemTypeCode,
            invoiceItem.Amount);

        invoiceItem.ItemObligations.AddRange(
            command.ToItemObligations(incotermPairs));
    }
}
