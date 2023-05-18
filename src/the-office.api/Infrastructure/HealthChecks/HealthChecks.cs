using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace the_office.api.Infrastructure.HealthChecks;

public static class HealthChecks
{
    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health",
            new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = new
                    {
                        status = report.Status.ToString(),
                        errors = report.Entries.Select(e => new
                        {
                            key = e.Key,
                            value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                        })
                    };

                    var jsonResponse = JsonSerializer.Serialize(result);

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(jsonResponse);
                }
            });

        return app;
    }
}