using Customer.Application.Interface;
using Customer.Infrastructure.Configurations;
using Customer.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomerInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration["CosmosDb:ConnectionString"] ?? throw new InvalidOperationException("Cosmos db connection string is not available");
            //var databaseName = configuration["CosmosDb:DatabaseName"] ?? throw new InvalidOperationException("Database information is not available");
            //var containerName = configuration["CosmosDb:ContainerName"] ?? throw new InvalidOperationException("Container details are not available");

            serviceCollection.AddSingleton<CosmosClient>(sp =>
            {
                var configuration =
                    sp.GetRequiredService<IConfiguration>();

                var connectionString =
                    configuration["CosmosDb:ConnectionString"]
                    ?? throw new InvalidOperationException(
                        "Cosmos DB connection string is missing.");

                return new CosmosClient(connectionString);
            });
            
            serviceCollection.AddSingleton<CosmosDbContext>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddSingleton<CosmosDbInitializer>();
            return serviceCollection;
        }
    }
}
