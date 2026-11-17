using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations
        : IEntityTypeConfiguration<Order.Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Order.Domain.Entities.Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.OrderAt)
                .IsRequired();

            // Money value object
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

            // Order -> OrderItems relationship
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(o => o.OrderItems)
                .HasField("_orderItems")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}