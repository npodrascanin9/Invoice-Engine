using InvoiceEngine.API.Features.Invoices.Create;

namespace InvoiceEngine.API.IntegrationTests.Features.Invoices.Create;

public class CreateInvoiceTests :
    BaseIntegrationTest
{
    private readonly Faker<CreateInvoiceCommand> _commandGenerator 
        = new Faker<CreateInvoiceCommand>()
            .CustomInstantiator(factoryMethod => new CreateInvoiceCommand(
                Incoterm: factoryMethod.PickRandom<IncotermRule>(),
                IssuedAt: DateOnly.FromDateTime(factoryMethod.Date.Recent(5)),
                ExpiresAt: DateOnly.FromDateTime(factoryMethod.Date.Soon(30)),
                TransactionStartDate: DateOnly.FromDateTime(factoryMethod.Date.Recent(10)),
                TransactionEndDate: DateOnly.FromDateTime(factoryMethod.Date.Soon(20)),
                ClientSellerId: 1,
                ClientBuyerId: 2,
                Items: new Dictionary<InvoiceItemTypeCode, CreateInvoiceItemRequest>
                {
                    { InvoiceItemTypeCode.SellGoods, new CreateInvoiceItemRequest(factoryMethod.Commerce.ProductName(), factoryMethod.Random.Decimal(100, 2000)) },
                    { InvoiceItemTypeCode.Transport, new CreateInvoiceItemRequest("Transport service", factoryMethod.Random.Decimal(50, 500)) },
                    { InvoiceItemTypeCode.Insurance, new CreateInvoiceItemRequest("Insurance policy", factoryMethod.Random.Decimal(20, 300)) }
                },
                CustomIncotermRules: null
            ));

    public CreateInvoiceTests(
        IntegrationTestWebAppFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task ShouldCreateInvoice()
    {
        // Arrange
        var command = _commandGenerator.Generate();

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
