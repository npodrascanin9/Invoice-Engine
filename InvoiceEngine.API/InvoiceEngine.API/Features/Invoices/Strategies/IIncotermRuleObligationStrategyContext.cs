namespace InvoiceEngine.API.Features.Invoices.Strategies;

public interface IIncotermRuleObligationStrategyContext
{
    Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        IncotermRule incotermRule,
        InvoiceItemTypeCode invoiceItemType,
        decimal amount,
        Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>> customIncotermRules = null);
}

public class IncotermRuleObligationStrategyContext(
    IEnumerable<IIncotermRuleObligationStrategy> strategies) :
    IIncotermRuleObligationStrategyContext
{
    public Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        IncotermRule incotermRule,
        InvoiceItemTypeCode invoiceItemType,
        decimal amount,
        Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject to), decimal>> customIncotermRules = null)
    {
        var dictionary = strategies.ToDictionary(
            key => key.IncotermRule,
            value => value);

        if (!dictionary.TryGetValue(incotermRule, out var strategy))
            throw new NotSupportedException($"Incoterm rule {incotermRule} is not supported.");

        return strategy.ResolveIncotermObligation(
            invoiceItemType, 
            amount,
            customIncotermRules);
    }
}
