using the_office.api.Configurations;
using the_office.api.Infrastructure.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies(builder.Host);

// Swagger Config
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerSetup();

// app.UseHealthChecks();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();