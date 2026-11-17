using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Infrastructure.Persistence
{
    public class CustomerRepository : Customer.Application.Interface.ICustomerRepository
    {
        private readonly Container _cosmosContainer;
        public CustomerRepository(CosmosDbContext cosmosDbContext)
        {
            // Initialize the repository with the CosmosDbContext
            _cosmosContainer = cosmosDbContext.CosmosContainer;

        }
        public async Task AddAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default)
        {
            await _cosmosContainer.CreateItemAsync(customer, new PartitionKey(customer.Id.ToString()), cancellationToken: cancellationToken);
        }

        public async Task<Domain.Entities.Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _cosmosContainer.ReadItemAsync<Domain.Entities.Customer>(customerId.ToString(), new PartitionKey(customerId.ToString()), cancellationToken: cancellationToken);
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Handle the case where the customer is not found
                return null;
            }
        }

        public async Task UpdateAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default)
        {
            await _cosmosContainer.UpsertItemAsync<Domain.Entities.Customer>(customer, new PartitionKey(customer.CustomerId.ToString()), cancellationToken: cancellationToken);
        }
    }
}
