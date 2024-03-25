using MassTransit;
using Ordering.Application;
using Ordering.Api.Extensions;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using EventBus.Messages.Common;
using Ordering.Api.EventBusConsumer;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Mapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((context, config) =>
    {
        config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(context);
        });
        config.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddScoped<BasketCheckoutConsumer>();

var app = builder.Build();

app.MigrateDatabase<OrderDbContext>(5);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();