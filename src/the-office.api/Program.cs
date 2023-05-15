using Microsoft.EntityFrameworkCore;
using the_office.api.Configurations;
using the_office.insfrastructure.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("TheOfficeConnectionString");

builder.Services.AddDbContext<TheOfficedbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

//var config = builder.Configuration;
//builder.Services.Configure <[Sua_Classe] > (config);
//var settings = config.Get <[Sua_Classe] > ();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Office API");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
