using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using the_office.infrastructure.Data.Context;

namespace the_office.api.Infrastructure.HealthChecks;

public static class HealthChecks
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddHealthChecks()
            .AddDbContextCheck<TheOfficeDbContext>(); 

        return services;
    }
    
    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health",
            new HealthCheckOptions()
            {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}