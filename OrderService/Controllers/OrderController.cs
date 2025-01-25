using LibraryMessages.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController(IBus bus) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(OrderDto orderDto)
        {
            var orderPlaced = new OrderPlaced(orderDto.OrderId, orderDto.Quantity);
            await bus.Publish(orderPlaced, context =>
            {
                context.SetRoutingKey(orderPlaced.Quantity > 10 ? "order.shipping" : "order.Tracking");
            });
            return NoContent();
        }
    }

}
