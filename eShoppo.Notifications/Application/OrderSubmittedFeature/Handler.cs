using System.Text.Json;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderSubmittedFeature;

internal class Handler : IConsumer<OrderSubmitted>
{
    private readonly ILogger<Handler> _logger;

    public Handler(ILogger<Handler> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = context.Message;
        var message = JsonSerializer.Serialize(order.OrderItems);
        _logger.LogInformation($"Notification about order {order.OrderNumber} submitting is sent to user {order.CustomerId}: \nContent:{message}");
    }
}