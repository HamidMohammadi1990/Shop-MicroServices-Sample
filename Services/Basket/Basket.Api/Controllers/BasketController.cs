using System.Net;
using AutoMapper;
using MassTransit;
using Basket.Api.Models;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using EventBus.Messages.Events;

namespace Basket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController
    (IBasketRepository basketRepository, DiscountGrpcService discountGrpcService,
    IMapper mapper, IPublishEndpoint publishEndpoint)
    : ControllerBase
{
    [HttpGet("{userName}", Name = "get-basket")]
    public async Task<ShoppingCart?> GetUserBasket(string userName)
    {
        return await basketRepository.GetUserBasket(userName) ?? new ShoppingCart(userName);
    }

    [HttpPost("update-basket")]
    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
    {
        //foreach (var item in shoppingCart.Items)
        //{
        //    var coupon = await discountGrpcService.GetDiscount(item.ProductName);
        //    item.Price -= coupon.Amount;
        //}
        return await basketRepository.UpdateBasket(shoppingCart);
    }

    [HttpDelete("{userName}", Name = "delete-basket")]
    public async Task<bool> DeleteBasket(string userName)
    {
        await basketRepository.DeleteBasket(userName);
        return true;
    }

    [HttpPost("checkout-basket")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout(BasketCheckout request)
    {
        var basket = await basketRepository.GetUserBasket(request.UserName);
        if (basket is null)
        {
            return BadRequest();
        }

        var eventMessage = mapper.Map<BasketCheckoutEvent>(request);        
        await publishEndpoint.Publish(eventMessage);

        await basketRepository.DeleteBasket(request.UserName);
        return Accepted();
    }
}