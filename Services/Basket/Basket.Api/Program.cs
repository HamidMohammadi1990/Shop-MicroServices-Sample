using MassTransit;
using Discount.Grpc.Protos;
using Basket.Api.Repositories;
using Basket.Api.GrpcServices;
using Basket.Api.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
        options.Configuration = builder.Configuration.GetValue<string>("RedisSettings:ConnectionString")
    );

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:Discounturl")!);
});

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(typeof(BasketMapperProfile));

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