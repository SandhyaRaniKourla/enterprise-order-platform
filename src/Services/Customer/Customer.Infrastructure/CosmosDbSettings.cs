using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Infrastructure
{
    public sealed class CosmosDbSettings
    {
        public string Endpoint { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = "EnterpriseOrders";
        public string ContainerName { get; set; } = "Customers";
    }
}
