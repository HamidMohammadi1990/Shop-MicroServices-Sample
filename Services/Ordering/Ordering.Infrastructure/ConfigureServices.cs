using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Providers.Email;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Contracts.Infrastructure;

namespace Ordering.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var CONNECTION = configuration.GetConnectionString("OrderingConnectionString");
        Console.WriteLine($"connection is {CONNECTION}");
        services.AddDbContext<OrderDbContext>(
            options => options.UseSqlServer(CONNECTION));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
} 