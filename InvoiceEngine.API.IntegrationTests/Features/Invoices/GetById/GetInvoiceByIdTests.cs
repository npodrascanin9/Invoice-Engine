using InvoiceEngine.API.Features.Invoices.GetById;

namespace InvoiceEngine.API.IntegrationTests.Features.Invoices.GetById;

[Collection(TestCollectionConstants.DefaultCollection)]
public class GetInvoiceByIdTests :
    BaseIntegrationTest,
    IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;

    public GetInvoiceByIdTests(
        IntegrationTestWebAppFactory apiFactory)
        : base(apiFactory)
    {
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task ShouldReturnData()
    {
        // Arrange
        var query = new GetInvoiceByIdQuery(
            Id: 1);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should()
            .NotBeNull();
        result.Value.Should()
            .NotBeNull();
        result.Value.Id.Should()
            .BeGreaterThan(0);
    }

    public Task InitializeAsync()
        => Task.CompletedTask;

    public Task DisposeAsync()
        => _resetDatabase();
}
