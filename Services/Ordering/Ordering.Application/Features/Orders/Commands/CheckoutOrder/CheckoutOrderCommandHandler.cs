using MediatR;
using AutoMapper;
using Ordering.Domain.Entities;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Contracts.Infrastructure;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService) : IRequestHandler<CheckoutOrderCommand, int>
{    
    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderModel = mapper.Map<Order>(request);
        var order = await orderRepository.CreateAsync(orderModel);
        return order.Id;
    }
}