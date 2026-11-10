using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Persistence.Configurations
{
    public sealed class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.ProductId).IsRequired();
            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.UnitPrice).HasPrecision(18, 2).IsRequired();
        }
    }
}
