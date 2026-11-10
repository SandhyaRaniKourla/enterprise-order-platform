using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstractions.Persistence;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrdersDatabase");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Orders connection string is not configured");
            }
            services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
