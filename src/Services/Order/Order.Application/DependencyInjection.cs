using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateOrderCommandHandler>();
            services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

            return services;
        }
    }
}
