using InvoiceEngine.API.Database;

namespace InvoiceEngine.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddSqlDatabaseConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            context => context.UseSqlServer(
                configuration.GetConnectionString("DefaultDb")));

        return services;
    }
}
