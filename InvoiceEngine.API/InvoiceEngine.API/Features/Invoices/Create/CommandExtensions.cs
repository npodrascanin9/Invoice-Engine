namespace InvoiceEngine.API.Features.Invoices.Create;

public static class CreateInvoiceCommandExtensions
{
    public static Invoice ToInvoice(
        this CreateInvoiceCommand command)
    {
        return new()
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
    }

    public static bool CanHaveCustomIncotermObligations(
        this CreateInvoiceCommand command)
    {
        bool isCustomIncoterm = command.Incoterm == IncotermRule.Custom;
        bool hasCustomDictionary = command.CustomIncotermRules is not null;
        return isCustomIncoterm && hasCustomDictionary;
    }

    public static List<InvoiceCustomIncotermObligation> ToCustomIncotermObligations(
        this CreateInvoiceCommand command)
    {
        return command
            .CustomIncotermRules
            .SelectMany(outerPairs =>
                outerPairs.Value.Select(
                    innerPairs => new InvoiceCustomIncotermObligation
                    {
                        ItemTypeCode = outerPairs.Key,
                        FromSubjectCode = innerPairs.Key.From,
                        ToSubjectCode = innerPairs.Key.To,
                        PercentageAmount = innerPairs.Value
                    })).ToList();
    }

    public static List<InvoiceClient> ToInvoiceClients(
        this CreateInvoiceCommand command)
    {
        return new List<InvoiceClient>()
        {
            new()
            {
                ClientId = command.ClientBuyerId,
                SubjectCode = InvoiceSubject.Buyer
            },
            new()
            {
                ClientId = command.ClientSellerId,
                SubjectCode = InvoiceSubject.Seller
            }
        };
    }

    public static List<InvoiceItem> ToInvoiceItems(
        this CreateInvoiceCommand command)
    {
        return command.Items
            .Where(x => x.Value is not null && x.Value.Amount > 0)
            .Select(x => new InvoiceItem()
            {
                Id = 0,
                ItemTypeCode = x.Key,
                Description = x.Value.Description,
                Amount = x.Value.Amount
            }).ToList();
    }

    public static List<InvoiceItemObligation> ToItemObligations(
        this CreateInvoiceCommand command,
        Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> incotermValuePairs)
    {
        return incotermValuePairs.Select(
            incotermPair => new InvoiceItemObligation
            {
                FromClientSubjectCode = incotermPair.Key.From,
                ToClientSubjectCode = incotermPair.Key.To,
                OwingAmount = incotermPair.Value
            }).ToList();
    }
}
