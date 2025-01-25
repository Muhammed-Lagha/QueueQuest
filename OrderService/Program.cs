using LibraryMessages.Messages;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.Message<OrderPlaced>(x => x.SetEntityName("order-placed-exchange-fanout"));

        // define the exchange as a fanout exchange
        cfg.Publish<OrderPlaced>(x => x.ExchangeType = "fanout");

        // define the exchange as a direct exchange
        cfg.Publish<OrderPlaced>(x => x.ExchangeType = "direct");
    });
});

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
