namespace InvoiceEngine.API.UnitTests.Shared.Helpers;

public class PercentageCalculatorTests : BaseUnitTest
{
    [Test]
    [TestCase(1000, 25, 250)]
    [TestCase(1000, 50, 500)]
    [TestCase(1000, 100, 1000)]
    [TestCase(1000, 0, 0)]
    public void ShouldReturnExpectedResult(
        decimal amount,
        decimal percentage,
        decimal expectedResult)
    {
        // Act
        var result = PercentageCalculator.ApplyPercentage(amount, percentage);

        // Assert
        result.Should()
            .Be(expectedResult);
    }
}
