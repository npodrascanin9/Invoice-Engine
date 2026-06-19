namespace InvoiceEngine.API.UnitTests.Features.Invoices.Create;

public class CreateInvoiceCommandValidatorTests : 
    BaseUnitTest
{
    private readonly CreateInvoiceCommandValidator _validator = new();

    [Test]
    public void ShouldFail_WhenExpiresAtIsBeforeIssuedAt()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.EXW,
            IssuedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            ExpiresAt: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            TransactionStartDate: DateOnly.FromDateTime(DateTime.UtcNow),
            TransactionEndDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ClientSellerId: 1,
            ClientBuyerId: 2,
            Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
            {
                [InvoiceItemTypeCode.SellGoods] = new CreateInvoiceItemRequest(
                    Amount: 100, 
                    Description: "Valid")
            }
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should()
            .BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage == "ExpiresAt must be greater than IssuedAt");
    }

    [Test]
    public void ShouldFail_WhenBuyerEqualsSeller()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.EXW,
            IssuedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            ExpiresAt: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TransactionStartDate: DateOnly.FromDateTime(DateTime.UtcNow),
            TransactionEndDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ClientSellerId: 1,
            ClientBuyerId: 1,
            Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
            {
                [InvoiceItemTypeCode.SellGoods] = new CreateInvoiceItemRequest(
                    Amount: 100, 
                    Description: "Valid")
            }
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should()
            .BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage == "Buyer and Seller must be different clients");
    }

    [Test]
    public void ShouldFail_WhenCustomIncotermWithoutRules()
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
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should()
            .BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage == "CustomIncotermDetails must be provided when using a Custom Incoterm.");
    }

    [Test]
    public void ShouldPass_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new CreateInvoiceCommand(
            Incoterm: IncotermRule.EXW,
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
                    Description: "Valid item")
            },
            CustomIncotermRules: null
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should()
            .BeTrue();
    }
}
