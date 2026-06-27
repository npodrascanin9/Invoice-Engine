namespace InvoiceEngine.API.Features.Invoices.Strategies;

public class FobIncotermRuleObligationStrategy :
    IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule
        => IncotermRule.FOB;


    private Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>> _map =
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
                    { (From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller), 50 },
                    { (From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer), 50 }
                }
            },
            {
                InvoiceItemTypeCode.Insurance,
                new()
                {
                    { (From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller), 50 },
                    { (From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer), 50 }
                }
            }
        };

    public Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType, 
        decimal amount,
        Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>> customIncotermRules = null)
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
