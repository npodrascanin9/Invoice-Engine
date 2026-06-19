using InvoiceEngine.API.Features.Invoices.Create;

namespace InvoiceEngine.API.IntegrationTests.Features.Invoices.Create;

public class CreateInvoiceTests :
    BaseIntegrationTest
{
    public CreateInvoiceTests(
        IntegrationTestWebAppFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task ShouldCreateInvoice()
    {
        // Arrange
        var command = new CreateInvoiceCommand();

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should()
            .NotBeNull();
        var foundInvoice = DbContext
            .Invoices
            .FirstOrDefault(x => x.Id == result.Value.Id);
    }
}
