using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands.CreateOrder;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateOrderResult), 201)]
        public async Task<IActionResult> CreateOrder([FromBody] Contracts.CreateOrderRequest request, [FromServices] CreateOrderCommandHandler handler, CancellationToken cancellationToken)
        {
            // Implementation for creating an order
            var command = new CreateOrderCommand(request.CustomerId, request.Items.Select(i => new CreateOrderItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList(), request.currency);
            var result = await handler.HandleAsync(command, cancellationToken);
            return CreatedAtAction(nameof(CreateOrder), new { id = result.OrderId }, result);
        }
    }
    public sealed record CreateOrderResult(
    Guid OrderId,
    string Status,
    decimal TotalAmount,
    string Currency);
}
