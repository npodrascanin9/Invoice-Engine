namespace InvoiceEngine.API.UnitTests.Features.Invoices.Strategies;

public class FobIncotermRuleObligationStrategyTests
{
    [Theory]
    [InlineData(InvoiceItemTypeCode.SellGoods, 1500, 1500, 0)]
    [InlineData(InvoiceItemTypeCode.Transport, 500, 250, 250)]
    [InlineData(InvoiceItemTypeCode.Insurance, 300, 150, 150)]
    public void ShouldReturnExpectedResult(
        InvoiceItemTypeCode itemTypeCode,
        decimal amount,
        decimal expectedBuyerToSellerAmount,
        decimal expectedSellerToBuyerAmount)
    {
        // Arrange
        FobIncotermRuleObligationStrategy strategy = new();

        // Act
        var result = strategy.ResolveIncotermObligation(
            itemTypeCode,
            amount);

        // Assert
        strategy.IncotermRule.Should()
            .Be(IncotermRule.FOB);
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
