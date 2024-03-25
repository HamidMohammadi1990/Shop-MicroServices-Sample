using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(ISender sender) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<int> Checkout(CheckoutOrderCommand request)
        => await sender.Send(request);

    [HttpPost("user-orders")]
    public async Task<List<GetOrdersResponse>> GetUserOrers(GetOrdersQuery request)
       => await sender.Send(request);
}