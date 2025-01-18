using LibraryMessages.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController(IBus _bus) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(OrderDto orderDto)
        {
            var orderPlaced = new OrderPlaced(orderDto.OrderId, orderDto.Quantity);
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:order-placed"));
            await endpoint.Send(orderPlaced);
            return NoContent();
        }
    }

}
