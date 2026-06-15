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
            x => x.Id == command.ClientSellerId))
        {
            return Result.Failure<CreateInvoiceResponse>(
                InvoiceErrors.ClientSellerNotFound(command.ClientSellerId));
        }

        if (!await context.Clients.AnyAsync(
            x => x.Id == command.ClientBuyerId))
        {
            return Result.Failure<CreateInvoiceResponse>(
                InvoiceErrors.ClientBuyerNotFound(command.ClientBuyerId));
        }

        Invoice invoiceEntity = new()
        {
            Id = default,
            IncotermCode = command.Incoterm,
            StatusCode = InvoiceStatus.Pending,
            ExpiresAt = command.ExpiresAt,
            IssuedAt = command.IssuedAt,
            TransactionStartDate = command.TransactionStartDate,
            TransactionEndDate = command.TransactionEndDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        invoiceEntity.Items.AddRange(
            command.Items
                .Where(x => x.Value is not null && x.Value.Amount > 0)
                .Select(x => new InvoiceItem()
                {
                    Id = 0,
                    ItemTypeCode = x.Key,
                    Description = x.Value.Description,
                    Amount = x.Value.Amount
                }).ToList());

        foreach (var invoiceItem in invoiceEntity.Items)
            InitializeItem(command, invoiceItem);

        await context.Invoices.AddAsync(invoiceEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateInvoiceResponse>(
            new(invoiceEntity.Id));
    }

    private void InitializeItem(
        CreateInvoiceCommand command, 
        InvoiceItem invoiceItem)
    {
        var dictionary = obligationStrategyContext.ResolveIncotermObligation(
            command.Incoterm,
            invoiceItem.ItemTypeCode,
            invoiceItem.Amount);

        invoiceItem
            .ItemObligations
            .AddRange(
                dictionary.Select(
                    x => new InvoiceItemObligation
                    {
                        FromClientSubjectCode = x.Key.From,
                        ToClientSubjectCode = x.Key.To,
                        OwingAmount = x.Value
                    }).ToList());
    }
}
