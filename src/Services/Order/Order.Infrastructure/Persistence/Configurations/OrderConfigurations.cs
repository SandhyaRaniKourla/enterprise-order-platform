using Microsoft.EntityFrameworkCore;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order.Domain.Entities.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order.Domain.Entities.Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.Status)
                .HasConversion<String>()
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(o => o.OrderAt).IsRequired();
            builder.OwnsOne(order => order.Total, money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("total_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(value => value.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Navigation(order => order.Total)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(o => o.OrderItems).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
