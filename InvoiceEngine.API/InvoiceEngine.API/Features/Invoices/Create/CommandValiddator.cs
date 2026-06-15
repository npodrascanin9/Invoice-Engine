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

        RuleFor(x => x.ClientBuyerId)
            .NotEqual(x => x.ClientSellerId)
            .WithMessage("Buyer and Seller must be different clients");

        RuleFor(x => x.ClientBuyerId)
            .GreaterThan(0).WithMessage("ClientBuyerId must be greater than 0");

        RuleFor(x => x.ClientSellerId)
            .GreaterThan(0).WithMessage("ClientSellerId must be greater than 0");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one item is required");

        RuleForEach(x => x.Items.Values)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.Amount)
                    .GreaterThan(0).WithMessage("Item amount must be greater than zero");

                item.RuleFor(i => i.Description)
                    .NotEmpty().WithMessage("Item description is required");
            });
    }
}
