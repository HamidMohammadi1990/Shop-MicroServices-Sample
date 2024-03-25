using MediatR;
using AutoMapper;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler
    (IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrdersQuery, List<GetOrdersResponse>>
{
    public async Task<List<GetOrdersResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByUserName(request.UserName);
        return mapper.Map<List<GetOrdersResponse>>(orders);
    }
}