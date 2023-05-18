using the_office.api.Configurations;
using the_office.api.Infrastructure.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddControllers();

// var connectionString = builder.Configuration.GetConnectionString("TheOfficeConnectionString");
//
// builder.Services.AddDbContext<TheOfficeDbContext>(options =>
// {
//     options.UseNpgsql(connectionString);
// });

//var config = builder.Configuration;
//builder.Services.Configure <[Sua_Classe] > (config);
//var settings = config.Get <[Sua_Classe] > ();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();

// Swagger Config
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerSetup();

// app.UseHealthChecks();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();