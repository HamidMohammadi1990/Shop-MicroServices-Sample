using AutoMapper;
using Basket.Api.Models;
using EventBus.Messages.Events;

namespace Basket.Api.Mapper;

public class BasketMapperProfile : Profile
{
    public BasketMapperProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}