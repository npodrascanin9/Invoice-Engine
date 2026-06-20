using InvoiceEngine.API.Features.Invoices.Create;

namespace InvoiceEngine.API.IntegrationTests.Features.Invoices.Create;

[Collection(TestCollectionConstants.DefaultCollection)]
public class CreateInvoiceTests :
    BaseIntegrationTest,
    IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;

    private readonly Faker<CreateInvoiceCommand> _commandGenerator 
        = new Faker<CreateInvoiceCommand>()
            .CustomInstantiator(factoryMethod => new CreateInvoiceCommand(
                Incoterm: factoryMethod.PickRandom(Enum.GetValues<IncotermRule>().Where(x => x != IncotermRule.Custom)),
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
        IntegrationTestWebAppFactory apiFactory)
        : base(apiFactory)
    {
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task ShouldCreateInvoice()
    {
        // Arrange
        var clients = new List<Client>()
        {
            new()
            {
                Id = 0,
                Email = "asdf@gmail.com",
                IdentificationNumber = "Id_1",
                IsActive = true,
                Name = "Number 1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt=  DateTime.UtcNow
            },
            new()
            {
                Id = 0,
                Email = "number2@gmail.com",
                IdentificationNumber = "Id_2",
                IsActive = true,
                Name = "Number 2",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt=  DateTime.UtcNow
            }
        };
        await DbContext.Clients.AddRangeAsync(
            clients);
        await DbContext.SaveChangesAsync();

        var command = _commandGenerator
            .RuleFor(x => x.ClientBuyerId, clients[0].Id)
            .RuleFor(x => x.ClientSellerId, clients[1].Id)
            .Generate();

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should()
            .NotBeNull();
        result.Value.Should()
            .NotBeNull();

        int invoiceId = result.Value.Id;
        
        invoiceId.Should()
            .BeGreaterThan(0);
        
        var foundInvoice = await DbContext
            .Invoices
            .Include(x => x.InvoiceClients)
            .Include(x => x.CustomIncotermObligations)
            .Include(x => x.Items)
            .ThenInclude(item => item.ItemObligations)
            .FirstOrDefaultAsync(x => x.Id == invoiceId);
        
        foundInvoice.Should()
            .NotBeNull();
        foundInvoice.InvoiceClients.Should()
            .HaveCount(2);
        foundInvoice.CustomIncotermObligations.Should()
            .BeNullOrEmpty();
        foundInvoice.Items.Should()
            .HaveCount(3);

        var obligations = foundInvoice.Items
            .SelectMany(i => i.ItemObligations)
            .ToList();

        obligations.Should()
            .HaveCount(6);
    }

    public Task InitializeAsync()
        => Task.CompletedTask;

    public Task DisposeAsync()
        => _resetDatabase();
}
