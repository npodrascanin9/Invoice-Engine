namespace InvoiceEngine.API.UnitTests.Features.Invoices.Strategies;

public class IncotermRuleObligationStrategyContextTests :
    BaseUnitTest
{
    [Test]
    public void ShouldDelegateToCorrectStrategy()
    {
        // Arrange
        var fakeStrategy = new Mock<IIncotermRuleObligationStrategy>();
        decimal amount = 150m;
        fakeStrategy
            .Setup(s => s.IncotermRule)
            .Returns(IncotermRule.CIF);
        
        fakeStrategy
            .Setup(s => s.ResolveIncotermObligation(
                InvoiceItemTypeCode.SellGoods, 
                100m, 
                null)
            ).Returns(new Dictionary<(InvoiceSubject, InvoiceSubject), decimal>
            {
                [(InvoiceSubject.Buyer, InvoiceSubject.Seller)] = amount
            });

        var context = new IncotermRuleObligationStrategyContext(
            new[] 
            { 
                fakeStrategy.Object 
            });

        // Act
        var result = context.ResolveIncotermObligation(
            IncotermRule.CIF, 
            InvoiceItemTypeCode.SellGoods, 
            100m);

        // Assert
        result.Should()
            .ContainKey((InvoiceSubject.Buyer, InvoiceSubject.Seller))
            .WhoseValue.Should().Be(amount);
    }
}
