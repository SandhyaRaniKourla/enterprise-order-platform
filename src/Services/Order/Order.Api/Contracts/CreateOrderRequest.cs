namespace Order.Api.Contracts
{
    public sealed record CreateOrderRequest(Guid CustomerId, IReadOnlyCollection<CreateOrderItemItem> Items, string currency);
    public sealed record CreateOrderItemItem(Guid ProductId, int Quantity, decimal UnitPrice);
}
