using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderCancelledFeature;

internal class Handler : IConsumer<OrderCancelled>
{
    private readonly ILogger<Handler> _logger;

    public Handler(ILogger<Handler> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = context.Message;
        _logger.LogInformation($"Notification about order {order.OrderNumber} cancellation is sent to user {order.CustomerId}");
    }
}