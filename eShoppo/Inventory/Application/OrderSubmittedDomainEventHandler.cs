using eShoppo.Orders.Domain;
using MassTransit;

namespace eShoppo.Inventory.Application;

public class OrderSubmittedDomainEventHandler : IConsumer<OrderSubmitted>
{
    private readonly ILogger<OrderSubmittedDomainEventHandler> _logger;

    public OrderSubmittedDomainEventHandler(ILogger<OrderSubmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        _logger.LogInformation($"Inventory is prepared for order {context.Message}");
        return Task.CompletedTask;
    }
}