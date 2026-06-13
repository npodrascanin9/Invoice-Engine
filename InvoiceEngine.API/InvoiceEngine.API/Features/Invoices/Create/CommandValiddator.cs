namespace InvoiceEngine.API.Features.Invoices.Create;

public class CreateInvoiceCommandValidator :
    AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.ExpiresAt)
            .GreaterThan(x => x.IssuedAt).WithMessage("ExpiresAt must be greater than IssuedAt");

        RuleFor(x => x.TransactionEndDate)
            .GreaterThan(x => x.TransactionStartDate).WithMessage("Transaction EndDate must be greater than StartDate");
    }
}
