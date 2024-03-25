using MediatR;
using AutoMapper;
using System.Reflection;
using Ordering.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}