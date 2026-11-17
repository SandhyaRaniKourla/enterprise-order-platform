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
            services.AddDbContext<OrderDbContext>(options => 
            {
                options.UseNpgsql(connectionString);
                // Suppress the PendingModelChangesWarning to allow application startup
                // when model changes haven't been migrated yet
                options.ConfigureWarnings(w => 
                    w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            });
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
