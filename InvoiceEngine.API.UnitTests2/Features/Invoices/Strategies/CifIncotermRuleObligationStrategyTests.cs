namespace InvoiceEngine.API.UnitTests.Features.Invoices.Strategies;

internal class CifIncotermRuleObligationStrategyTests :
    BaseUnitTest
{
    [Test]
    [TestCase(InvoiceItemTypeCode.SellGoods, 1500, 1500, 0)]
    [TestCase(InvoiceItemTypeCode.Transport, 650, 0, 650)]
    [TestCase(InvoiceItemTypeCode.Insurance, 300, 0, 300)]
    public void ShouldReturnExpectedResult(
        InvoiceItemTypeCode itemTypeCode,
        decimal amount,
        decimal expectedBuyerToSellerAmount,
        decimal expectedSellerToBuyerAmount)
    {
        // Arrange
        CifIncotermRuleObligationStrategy strategy = new();

        // Act
        var result = strategy.ResolveIncotermObligation(
            itemTypeCode,
            amount);

        // Assert
        strategy.IncotermRule.Should()
            .Be(IncotermRule.CIF);
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
}
