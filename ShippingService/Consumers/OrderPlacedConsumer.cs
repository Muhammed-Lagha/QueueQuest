using LibraryMessages.Messages;
using MassTransit;

namespace ShippingService.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlaced>
    {
        public Task Consume(ConsumeContext<OrderPlaced> context)
        {
            var orderPlaced = context.Message;
            Console.WriteLine($"Order {orderPlaced.OrderId} placed with quantity {orderPlaced.Quantity}");
            return Task.CompletedTask;
        }
    }
}
