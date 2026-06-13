using InvoiceEngine.API.Features.Invoices.Strategies;

namespace InvoiceEngine.API.Extensions;

public static class StrategyExtensions
{
    public static IServiceCollection AddStrategies(
        this IServiceCollection services)
    {
        services.AddScoped<IIncotermRuleObligationStrategy, ExwIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategy, CifIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategy, FobIncotermRuleObligationStrategy>();
        services.AddScoped<IIncotermRuleObligationStrategyContext, IncotermRuleObligationStrategyContext>();

        return services;
    }
}
