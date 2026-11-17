using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Infrastructure.Persistence
{
    public sealed class CosmosDbContext
    {

        public Database CosmosDatabase { get; }
        public Container CosmosContainer { get; }

        public CosmosDbContext(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["DatabaseName"] ?? throw new InvalidOperationException("Db details are not available");
            var containerName = configuration["ContainerName"] ?? throw new InvalidOperationException("container details are not available");
            CosmosDatabase = cosmosClient.GetDatabase(databaseName);
            CosmosContainer = cosmosClient.GetContainer(databaseName, containerName);            
        }


    }
}
