using InvoiceEngine.API.Features.Invoices.Create;
using InvoiceEngine.API.Features.Invoices.GetById;

namespace InvoiceEngine.API.IntegrationTests.Features.Invoices.GetById;

[Collection(TestCollectionConstants.DefaultCollection)]
public class GetInvoiceByIdTests :
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

    private readonly Faker<Client> _clientGenerator
        = new Faker<Client>()
            .CustomInstantiator(f => new Client
            {
                Id = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                Name = f.Company.CompanyName(),
                Email = f.Person.Email,
                IdentificationNumber = f.Random.Replace("##########")
            });

    public GetInvoiceByIdTests(
        IntegrationTestWebAppFactory apiFactory)
        : base(apiFactory)
    {
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task ShouldReturnNotFoundError()
    {
        // Arrange
        const int id = 1;
        var query = new GetInvoiceByIdQuery(
            Id: id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should()
            .NotBeNull();
        result.Error.Should()
            .NotBeNull();
        result.IsFailure.Should()
            .BeTrue();
        result.Error.Should()
            .BeEquivalentTo(InvoiceErrors.NotFound(id));
    }

    [Fact]
    public async Task ShouldReturnData()
    {
        // Arrange
        var clientEntities = _clientGenerator.Generate(2);
        await DbContext.Clients.AddRangeAsync(clientEntities);
        await DbContext.SaveChangesAsync();

        var createInvoiceResult = await Sender.Send(_commandGenerator
            .RuleFor(x => x.ClientBuyerId, clientEntities[0].Id)
            .RuleFor(x => x.ClientSellerId, clientEntities[1].Id)
            .Generate());

        var query = new GetInvoiceByIdQuery(
            createInvoiceResult.Value.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should()
            .NotBeNull();
        result.IsSuccess.Should()
            .BeTrue();
        result.Value.Should()
            .NotBeNull();

        var foundInvoice = await DbContext
            .Invoices
            .Include(x => x.InvoiceClients)
            .ThenInclude(invoiceClient => invoiceClient.Client)
            .Include(x => x.Items)
            .ThenInclude(item => item.ItemObligations)
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        foundInvoice.Should()
            .NotBeNull();

        var expectedResponse = InitializeExpectedResponse(
            foundInvoice, 
            query);

        result.Value.Should()
            .BeEquivalentTo(expectedResponse);
    }

    private static GetInvoiceByIdResponse InitializeExpectedResponse(
        Invoice foundInvoice, 
        GetInvoiceByIdQuery query)
    {
        var clients = foundInvoice.InvoiceClients.ToDictionary(
            ic => ic.SubjectCode,
            ic => new ClientInvoiceDetailsResponse(
                ClientId: ic.ClientId,
                Name: ic.Client.Name,
                Items: query.MapItemsForClient(ic.SubjectCode, foundInvoice.Items)));

        return new GetInvoiceByIdResponse(
            Id: foundInvoice.Id,
            IncotermRuleCode: foundInvoice.IncotermCode,
            IncotermRule: foundInvoice.IncotermCode.ToString(),
            StatusCode: foundInvoice.StatusCode,
            Status: foundInvoice.StatusCode.ToString(),
            IssuedAt: foundInvoice.IssuedAt,
            ExpiresAt: foundInvoice.ExpiresAt,
            TransactionStartDate: foundInvoice.TransactionStartDate,
            TransactionEndDate: foundInvoice.TransactionEndDate,
            CreatedAt: foundInvoice.CreatedAt,
            UpdatedAt: foundInvoice.UpdatedAt,
            Clients: clients
        );
    }

    public Task InitializeAsync()
        => Task.CompletedTask;

    public Task DisposeAsync()
        => _resetDatabase();
}
