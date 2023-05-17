using System.Reflection;
using Microsoft.EntityFrameworkCore;
using the_office.infrastructure.Data;

namespace the_office.api.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var connectionString = configuration.GetConnectionString(nameof(TheOfficeDbContext));

        services.AddDbContext<TheOfficeDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(Assembly.GetCallingAssembly().GetName().Name);
            });

            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}