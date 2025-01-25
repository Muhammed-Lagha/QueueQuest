using MassTransit;
using ShippingService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("shipping-order-queue", e =>
        {

            // bind the consumer to the fanout exchange
            e.Consumer<OrderPlacedConsumer>(context);
            e.Bind("order-placed-exchange-fanout", s =>
            {
                s.RoutingKey = "order.shipping";
                s.ExchangeType = "fanout";
            });

            // bind the consumer to the direct exchange
            //e.Consumer<OrderPlacedConsumer>(context);
            //e.Bind("order-placed-exchange", s =>
            //{
            //    s.RoutingKey = "order.shipping";
            //    s.ExchangeType = "direct";
            //});
        });
    });
});

builder.Services.AddScoped<ShippingService.Consumers.OrderPlacedConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
