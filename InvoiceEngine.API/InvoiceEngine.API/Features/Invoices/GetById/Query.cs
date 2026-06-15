namespace InvoiceEngine.API.Features.Invoices.GetById;

public record GetInvoiceByIdQuery(
    int Id) : IQuery<Result<GetInvoiceByIdResponse>>;
