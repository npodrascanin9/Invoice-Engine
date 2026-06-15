namespace InvoiceEngine.API.Features.Invoices.Strategies;

public interface IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule { get; }

    Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType,
        decimal amount);
}
