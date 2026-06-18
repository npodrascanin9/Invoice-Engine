namespace InvoiceEngine.API.Features.Invoices.Strategies;

public class CustomIncotermRuleObligationStrategy :
    IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule
        => IncotermRule.Custom;

    public Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType, 
        decimal amount,
        Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject to), decimal>> customIncotermRules = null)
    {
        ArgumentNullException.ThrowIfNull(customIncotermRules);

        if (!customIncotermRules.ContainsKey(invoiceItemType))
        {
            throw new ArgumentException(
                $"CustomIncotermRules doesn't contain details for InvoiceItemType='{invoiceItemType}'");
        }

        return customIncotermRules[invoiceItemType].ToDictionary(
            kvp => kvp.Key,
            kvp => PercentageCalculator.ApplyPercentage(amount, kvp.Value));
    }
}
