namespace InvoiceEngine.API.Features.Shared;

public static class FeatureDependencyInjection
{
    public static IServiceCollection AddFeatures(
        this IServiceCollection services)
    {
        services.RegisterInvoiceDependencies();

        return services;
    }
}
