namespace InvoiceEngine.API.UnitTests.Features.Invoices.Create;

public class CreateInvoiceMapperTests : 
    BaseUnitTest
{
    [Test]
    public void CanHaveCustomIncotermObligations_ShouldReturnTrue_WhenCustomIncotermAndRulesProvided()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.Custom,
            IssuedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            ExpiresAt: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TransactionStartDate: DateOnly.FromDateTime(DateTime.UtcNow),
            TransactionEndDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ClientSellerId: 1,
            ClientBuyerId: 2,
            Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
            {
                [InvoiceItemTypeCode.SellGoods] = new CreateInvoiceItemRequest(
                    Amount: 100,
                    Description: "Valid")
            },
            CustomIncotermRules: new Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject, InvoiceSubject), decimal>>
            {
                [InvoiceItemTypeCode.SellGoods] = new()
                {
                    [(InvoiceSubject.Buyer, InvoiceSubject.Seller)] = 50
                }
            }
        );

        // Act
        var result = command.CanHaveCustomIncotermObligations();
        
        // Assert
        result.Should()
            .BeTrue();
    }

    [Test]
    public void CanHaveCustomIncotermObligations_ShouldReturnFalse_WhenCustomIncotermWithoutRules()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.Custom,
            IssuedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            ExpiresAt: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TransactionStartDate: DateOnly.FromDateTime(DateTime.UtcNow),
            TransactionEndDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ClientSellerId: 1,
            ClientBuyerId: 2,
            Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
            {
                [InvoiceItemTypeCode.SellGoods] = new CreateInvoiceItemRequest(
                    Amount: 100,
                    Description: "Valid")
            },
            CustomIncotermRules: null
        );

        // Act
        var result = command.CanHaveCustomIncotermObligations();
        
        // Assert
        result.Should()
            .BeFalse();
    }

    [Test]
    public void CanHaveCustomIncotermObligations_ShouldReturnFalse_WhenStandardIncotermEvenWithRules()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.CIF,
            IssuedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            ExpiresAt: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TransactionStartDate: DateOnly.FromDateTime(DateTime.UtcNow),
            TransactionEndDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ClientSellerId: 1,
            ClientBuyerId: 2,
            Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
            {
                [InvoiceItemTypeCode.SellGoods] = new CreateInvoiceItemRequest(
                    Amount: 100, 
                    Description: "Valid")
            },
            CustomIncotermRules: new Dictionary<InvoiceItemTypeCode, Dictionary<(InvoiceSubject, InvoiceSubject), decimal>>
            {
                [InvoiceItemTypeCode.SellGoods] = new()
                {
                    [(InvoiceSubject.Buyer, InvoiceSubject.Seller)] = 50
                }
            }
        );

        // Act
        var result = command.CanHaveCustomIncotermObligations();
        
        // Assert
        result.Should()
            .BeFalse();
    }
}
