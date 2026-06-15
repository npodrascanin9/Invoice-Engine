namespace InvoiceEngine.API.Features.Invoices.Strategies;

public class CifIncotermRuleObligationStrategy :
    IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule
        => IncotermRule.CIF;

    private Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject to), decimal>> _map =
        new()
        {
            {
                InvoiceItemTypeCode.SellGoods,
                new()
                {
                    { (From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller), 100 },
                    { (From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer), 0 }
                }
            },
            {
                InvoiceItemTypeCode.Transport,
                new()
                {
                    { (From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller), 0 },
                    { (From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer), 100 }
                }
            },
            {
                InvoiceItemTypeCode.Insurance,
                new()
                {
                    { (From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller), 0 },
                    { (From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer), 100 }
                }
            }
        };

    public Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType, 
        decimal amount)
    {
        var pairs = _map[invoiceItemType];

        var result = new Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>();

        foreach (var pair in pairs)
        {
            var owingAmount = PercentageCalculator.ApplyPercentage(
                amount, 
                percentage: pair.Value);
            result[pair.Key] = owingAmount;
        }

        return result;
    }
}
