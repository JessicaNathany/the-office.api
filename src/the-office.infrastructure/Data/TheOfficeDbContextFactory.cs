using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace the_office.infrastructure.Data;

public class TheOfficeDbContextFactory: IDesignTimeDbContextFactory<TheOfficeDbContext>
{
    public TheOfficeDbContext CreateDbContext(string[] args)
    {
        var configuration = GetConfiguration();

        var builder = new DbContextOptionsBuilder<TheOfficeDbContext>();
 
        var connectionString = configuration.GetConnectionString(nameof(TheOfficeDbContext));
 
        builder.UseNpgsql(connectionString);
 
        return new TheOfficeDbContext(builder.Options);
    }
    
    private static IConfigurationRoot GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        return configuration;
    }
}