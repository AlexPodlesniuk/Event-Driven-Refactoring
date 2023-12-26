using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderPaidFeature;

internal class Handler : IConsumer<OrderPaid>
{
    private readonly ILogger<Handler> _logger;

    public Handler(ILogger<Handler> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderPaid> context)
    {
        var order = context.Message;
        _logger.LogInformation($"Notification about order {order.OrderNumber} successful payment is sent to user {order.CustomerId}");
    }
}