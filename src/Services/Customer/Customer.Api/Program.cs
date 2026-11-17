using Customer.Infrastructure;
using Customer.Infrastructure.Configurations;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCustomerInfrastructure(builder.Configuration);

var app = builder.Build();
var initializer = app.Services
    .GetRequiredService<CosmosDbInitializer>();

await initializer.InitializeAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
