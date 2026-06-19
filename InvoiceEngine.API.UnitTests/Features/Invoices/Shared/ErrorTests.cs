namespace InvoiceEngine.API.UnitTests.Features.Invoices.Shared;

public class InvoiceErrorTests
{
    [Fact]
    public void NotFound_ShouldReturnExpectedError()
    {
        // Arrange
        int id = 123;

        // Act
        var error = InvoiceErrors.NotFound(id);

        // Assert
        error.Code.Should()
            .Be("Invoices.NotFound");
        error.Description.Should()
            .Be($"Invoice with Id='{id}' not found");
    }

    [Fact]
    public void ClientBuyerNotFound_ShouldReturnExpectedError()
    {
        // Arrange
        int id = 456;

        // Act
        var error = InvoiceErrors.ClientBuyerNotFound(id);

        // Assert
        error.Code.Should()
            .Be("Invoices.NotFound");
        error.Description.Should()
            .Be($"Client buyer with Id='{id}' not found");
    }

    [Fact]
    public void ClientSellerNotFound_ShouldReturnExpectedError()
    {
        // Arrange
        int id = 789;

        // Act
        var error = InvoiceErrors.ClientSellerNotFound(id);

        // Assert
        error.Code.Should()
            .Be("Invoices.NotFound");
        error.Description.Should()
            .Be($"Client seller with Id='{id}' not found");
    }
}
