namespace InvoiceEngine.API.Features.Invoices.Strategies;

public class ExwIncotermRuleObligationStrategy :
    IIncotermRuleObligationStrategy
{
    public IncotermRule IncotermRule
        => IncotermRule.EXW;

    private Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal>> _map =
        new()
        {
            {
                InvoiceItemTypeCode.SellGoods,
                new()
                {
                    { (from: InvoiceSubject.Buyer, to: InvoiceSubject.Seller), 100 },
                    { (from: InvoiceSubject.Seller, to: InvoiceSubject.Buyer), 0 }
                }
            },
            {
                InvoiceItemTypeCode.Transport,
                new()
                {
                    { (from: InvoiceSubject.Buyer, to: InvoiceSubject.Seller), 100 },
                    { (from: InvoiceSubject.Seller, to: InvoiceSubject.Buyer), 0 }
                }
            },
            {
                InvoiceItemTypeCode.Insurance,
                new()
                {
                    { (from: InvoiceSubject.Buyer, to: InvoiceSubject.Seller), 100 },
                    { (from: InvoiceSubject.Seller, to: InvoiceSubject.Buyer), 0 }
                }
            }
        };

    public Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal> ResolveIncotermObligation(
        InvoiceItemTypeCode invoiceItemType, 
        decimal amount)
    {
        var pairs = _map[invoiceItemType];

        var result = new Dictionary<(InvoiceSubject from, InvoiceSubject to), decimal>();

        foreach (var pair in pairs)
        {
            var percentage = pair.Value;
            var owingAmount = PercentageCalculator.ApplyPercentage(amount, percentage);
            result[pair.Key] = owingAmount;
        }

        return result;
    }
}
