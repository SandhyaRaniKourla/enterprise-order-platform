using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Persistence
{
    public sealed class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Order.Domain.Entities.Order> Orders => Set<Order.Domain.Entities.Order>();
    }
}
