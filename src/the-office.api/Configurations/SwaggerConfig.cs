using Microsoft.OpenApi.Models;

namespace the_office.api.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "The Office API",
                Description = "The Office API Swagger surface"
            });
        });

        return services;
    }

    public static void UseSwaggerSetup(this WebApplication app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Office API");
            c.RoutePrefix = string.Empty;
        });
    }
}