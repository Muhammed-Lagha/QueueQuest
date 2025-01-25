using LibraryMessages.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController(IBus bus) : ControllerBase
    {

        // this endpoint is for the direct exchange , that uses the routing key to route the message to the consumer
        //[HttpPost]
        //public async Task<ActionResult> PlaceOrder(OrderDto orderDto)
        //{
        //    var orderPlaced = new OrderPlaced(orderDto.OrderId, orderDto.Quantity);
        //    await bus.Publish(orderPlaced, context =>
        //    {
        //        context.SetRoutingKey(orderPlaced.Quantity > 10 ? "order.shipping" : "order.Tracking");
        //    });
        //    return NoContent();
        //}

        // this endpoint is for the fanout exchange , that uses the exchange to route the message to the consumer
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(OrderDto orderDto)
        {
            var orderPlaced = new OrderPlaced(orderDto.OrderId, orderDto.Quantity);

            var endpoint = await bus.GetSendEndpoint(new Uri("exchange:order-placed-exchange-fanout"));
            await endpoint.Send(orderPlaced);

            return NoContent();
        }
    }

}
