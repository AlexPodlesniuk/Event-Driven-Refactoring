using eShoppo.Orders.Domain;
using MassTransit;

namespace eShoppo.Notifications.Application;

public class OrderSubmittedDomainEventHandler : IConsumer<OrderSubmitted>
{
    private readonly ILogger<OrderSubmittedDomainEventHandler> _logger;

    public OrderSubmittedDomainEventHandler(ILogger<OrderSubmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        _logger.LogInformation($"Notification is sent for order {context.Message}");
        return Task.CompletedTask;
    }
}