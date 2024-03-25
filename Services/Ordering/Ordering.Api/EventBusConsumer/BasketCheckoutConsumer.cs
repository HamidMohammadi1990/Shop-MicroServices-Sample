using MediatR;
using MassTransit;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketCheckoutConsumer(ISender sender, ILogger<BasketCheckoutConsumer> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var checkoutCommand = new CheckoutOrderCommand
        {
            Email = context.Message.Email,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            UserName = context.Message.UserName
        };
        var result = await sender.Send(checkoutCommand);
        logger.LogInformation($"order consumer is successfully with order id : {result}");
    }
}