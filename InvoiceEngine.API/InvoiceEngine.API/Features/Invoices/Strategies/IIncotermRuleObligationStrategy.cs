namespace InvoiceEngine.API.Features.Invoices.Strategies;

public interface IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule { get; }

    Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType,
        decimal amount);
}
