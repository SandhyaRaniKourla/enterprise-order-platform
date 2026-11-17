using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Infrastructure.Configurations
{
    public sealed class CosmosDbInitializer
    {
        private readonly CosmosClient _cosmosClient;
        private readonly CosmosDbSettings _cosmosDbSettings;

        public CosmosDbInitializer(CosmosClient cosmosClient,IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
            _cosmosDbSettings = cosmosDbSettings?.Value  ?? throw new ArgumentNullException(nameof(cosmosDbSettings));
        }
        public async Task InitializeAsync()
        {
            var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_cosmosDbSettings.DatabaseName);
            var database = databaseResponse.Database;
            await database.CreateContainerIfNotExistsAsync(_cosmosDbSettings.ContainerName, "/customerId");
        }
    }
}
