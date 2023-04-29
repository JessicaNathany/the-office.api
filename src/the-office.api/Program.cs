using Microsoft.EntityFrameworkCore;
using the_office.api.Configurations;
using the_office.insfrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TheOfficeContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TheOfficeConnectionString"));
});


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
