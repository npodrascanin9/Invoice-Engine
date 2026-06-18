namespace InvoiceEngine.API.UnitTests.Features.Invoices.Strategies;

public class ExwIncotermRuleObligationStrategyTests :
    BaseUnitTest
{
    [Test]
    [TestCase(InvoiceItemTypeCode.SellGoods, 1500, 1500, 0)]
    [TestCase(InvoiceItemTypeCode.Transport, 800, 800, 0)]
    [TestCase(InvoiceItemTypeCode.Insurance, 455, 455, 0)]
    public void ShouldReturnExpectedResult(
        InvoiceItemTypeCode itemTypeCode,
        decimal amount,
        decimal expectedBuyerToSellerAmount,
        decimal expectedSellerToBuyerAmount)
    {
        // Arrange
        ExwIncotermRuleObligationStrategy strategy = new();

        // Act
        var result = strategy.ResolveIncotermObligation(
            itemTypeCode,
            amount);

        // Assert
        strategy.IncotermRule.Should()
            .Be(IncotermRule.EXW);
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
