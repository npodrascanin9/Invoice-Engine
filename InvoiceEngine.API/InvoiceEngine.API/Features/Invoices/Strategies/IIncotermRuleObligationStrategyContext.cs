namespace InvoiceEngine.API.Features.Invoices.Strategies;

public interface IIncotermRuleObligationStrategyContext
{
    Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal> ResolveIncotermObligation(
        IncotermRule incotermRule,
        InvoiceItemTypeCode invoiceItemType,
        decimal amount);
}

public class IncotermRuleObligationStrategyContext(
    IEnumerable<IIncotermRuleObligationStrategy> strategies) :
    IIncotermRuleObligationStrategyContext
{
    public Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal> ResolveIncotermObligation(
        IncotermRule incotermRule,
        InvoiceItemTypeCode invoiceItemType,
        decimal amount)
    {
        var dictionary = strategies.ToDictionary(
            key => key.IncotermRule,
            value => value);

        if (!dictionary.TryGetValue(incotermRule, out var strategy))
            throw new NotSupportedException($"Incoterm rule {incotermRule} is not supported.");

        return strategy.ResolveIncotermObligation(
            invoiceItemType, 
            amount);
    }
}
