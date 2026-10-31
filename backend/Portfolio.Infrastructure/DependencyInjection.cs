using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Infrastructure.Persistence;

namespace Portfolio.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the EF Core MySQL database context and related infrastructure services.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="connectionString">MySQL connection string (never hardcoded).</param>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<PortfolioDbContext>(options =>
            options.UseMySQL(connectionString, mySql =>
                mySql.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName)));

        return services;
    }
}
