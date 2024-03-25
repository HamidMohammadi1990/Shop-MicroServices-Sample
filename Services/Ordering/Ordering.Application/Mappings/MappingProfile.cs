using AutoMapper;
using Ordering.Domain.Entities;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, GetOrdersResponse>().ReverseMap();
        CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
    }
}