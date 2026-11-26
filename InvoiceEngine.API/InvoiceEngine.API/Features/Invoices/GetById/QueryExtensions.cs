namespace InvoiceEngine.API.Features.Invoices.GetById;

public static class GetInvoiceByIdExtensions
{
    public static List<InvoiceDetailItemResponse> MapItemsForClient(
        this GetInvoiceByIdQuery query,
        InvoiceSubject subject,
        IEnumerable<InvoiceItem> items)
    {
        return items
            .SelectMany(
                item => item.ItemObligations
                    .Where(o => o.FromClientSubjectCode == subject)
                    .Select(o => new InvoiceDetailItemResponse(
                        Id: item.Id,
                        ItemTypeCode: item.ItemTypeCode,
                        ItemType: item.ItemTypeCode.ToString(),
                        Amount: o.OwingAmount,
                        PaysTo: o.ToClientSubjectCode.ToString()
                    ))
            ).ToList();
    }

    public static GetInvoiceByIdResponse ToResponse(
        this GetInvoiceByIdQuery query,
        Invoice invoice,
        Dictionary<InvoiceSubject, ClientInvoiceDetailsResponse> clientPairs)
    {
        return new GetInvoiceByIdResponse(
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
            Clients: clientPairs
        );
    }
}
