namespace InvoiceEngine.API.Features.Invoices.Shared;

public static class InvoiceDependencyInjection
{
    public static IServiceCollection RegisterInvoiceDependencies(
        this IServiceCollection services)
    {
        services.AddScoped<IIncotermRuleObligationStrategy, ExwIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategy, CifIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategy, FobIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategy, CustomIncotermRuleObligationStrategy>();

        services.AddScoped<IIncotermRuleObligationStrategyContext, IncotermRuleObligationStrategyContext>();

        return services;
    }
}
