using Serilog;
using the_office.api.Configurations;
using the_office.api.Filters;
using the_office.api.Infrastructure.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<AfterHandlerActionFilterAttribute>();
        options.Filters.Add<ApiExceptionFilterAttribute>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies(builder.Host);

// Swagger Config
builder.Services.AddSwaggerConfiguration();

// Health Check
builder.Services.AddHealthChecksConfiguration();

// Serilog Config
builder.Host.AddSerilogConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwaggerSetup();

app.UseHealthChecks();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();