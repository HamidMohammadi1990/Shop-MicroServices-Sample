using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<List<GetOrdersResponse>>
{
    public string UserName { get; set; } = null!;
}