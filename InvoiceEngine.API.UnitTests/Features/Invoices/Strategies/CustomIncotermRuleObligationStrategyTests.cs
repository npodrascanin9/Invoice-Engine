namespace InvoiceEngine.API.UnitTests.Features.Invoices.Strategies;

public class CustomIncotermRuleObligationStrategyTests :
    BaseUnitTest
{
    [Test]
    [TestCase(InvoiceItemTypeCode.SellGoods, 25, 75, 1000, 250, 750)]
    [TestCase(InvoiceItemTypeCode.Transport, 50, 50, 1000, 500, 500)]
    [TestCase(InvoiceItemTypeCode.SellGoods, 40, 60, 1000, 400, 600)]
    public void ShouldReturnExpectedResult(
        InvoiceItemTypeCode itemTypeCode,
        decimal buyerToSellerPercentage,
        decimal sellerToBuyerPercentage,
        decimal amount,
        decimal expectedBuyerToSellerAmount,
        decimal expectedSellerToBuyerAmount)
    {
        // Arrange
        var customRules = new Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>>
        {
            [itemTypeCode] = new Dictionary<(InvoiceSubject From, InvoiceSubject To), decimal>
            {
                [(From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller)] = buyerToSellerPercentage,
                [(From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer)] = sellerToBuyerPercentage
            }
        };
        var strategy = new CustomIncotermRuleObligationStrategy();

        // Act
        var result = strategy.ResolveIncotermObligation(
            itemTypeCode, 
            amount,
            customRules);

        // Act
        strategy.IncotermRule.Should()
            .Be(IncotermRule.Custom);
        result.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(2);
        result.Should()
            .ContainKey((From: InvoiceSubject.Buyer, To: InvoiceSubject.Seller))
            .WhoseValue.Should()
                .Be(expectedBuyerToSellerAmount);
        result.Should()
            .ContainKey((From: InvoiceSubject.Seller, To: InvoiceSubject.Buyer))
            .WhoseValue.Should()
                .Be(expectedSellerToBuyerAmount);
    }

    [Test]
    [TestCase(InvoiceItemTypeCode.SellGoods, 50)]
    [TestCase(InvoiceItemTypeCode.Transport, 20)]
    [TestCase(InvoiceItemTypeCode.Insurance, 100)]
    public void ShouldThrowNullException(
        InvoiceItemTypeCode itemTypeCode,
        decimal amount)
    {
        // Arrange
        var strategy = new CustomIncotermRuleObligationStrategy();

        // Act
        Action act = () => strategy.ResolveIncotermObligation(
            itemTypeCode,
            amount,
            customIncotermRules: null);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>();
    }
}
