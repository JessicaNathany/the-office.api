using Serilog;

namespace the_office.api.Configurations;

public static class SerilogConfig
{
    public static IHostBuilder AddSerilogConfiguration(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.UseSerilog((context, configureLogger) => 
            configureLogger.ReadFrom.Configuration(configuration));

        return hostBuilder;
    }
}