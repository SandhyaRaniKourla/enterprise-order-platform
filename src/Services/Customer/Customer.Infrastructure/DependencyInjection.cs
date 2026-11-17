using Customer.Application.Interface;
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
            //var connectionString = configuration["CosmosDb:ConnectionString"] ?? throw new InvalidOperationException("Cosmos db connection string is not available");
            //var databaseName = configuration["CosmosDb:DatabaseName"] ?? throw new InvalidOperationException("Database information is not available");
            //var containeerName = configuration["CosmosDb:ContainerName"] ?? throw new InvalidOperationException("Containeer details are not available");
            // serviceCollection.AddSingleton(_ => new CosmosClient(connectionString));
            
            serviceCollection.AddSingleton<CosmosDbSettings>();
            serviceCollection.AddSingleton<CosmosDbContext>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            return serviceCollection;
        }
    }
}
