using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//RabbitMQ Configuration , cfg = configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketCheckoutConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
         cfg.Host(builder.Configuration["EventBusSettings:RabbitMQHostAddress"]);
        cfg.ReceiveEndpoint(EventBusList.BasketChecoutQueue,c=>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(context);
            });
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
