namespace InvoiceEngine.API.IntegrationTests;

[CollectionDefinition(TestCollectionConstants.DefaultCollection)]
public class SharedTestCollection :
    ICollectionFixture<IntegrationTestWebAppFactory>
{

}
