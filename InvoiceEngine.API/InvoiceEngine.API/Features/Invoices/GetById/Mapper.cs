namespace InvoiceEngine.API.Features.Invoices.GetById;

public static class GetInvoiceByIdMapper
{
    public static List<InvoiceDetailItemResponse> MapItemsForClient(
        this GetInvoiceByIdQuery query,
        InvoiceSubject subject,
        IEnumerable<InvoiceItem> items)
    {
        return items
            .SelectMany(item => item.ItemObligations
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
}
