using eShoppo.Orders.Domain;
using MassTransit;

namespace eShoppo.Orders.Application;

public class OrderSubmittedDomainEventHandler : IConsumer<OrderSubmitted>
{
    private readonly ILogger<OrderSubmittedDomainEventHandler> _logger;

    public OrderSubmittedDomainEventHandler(ILogger<OrderSubmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        _logger.LogInformation($"Order history is created for order {context.Message}");
        return Task.CompletedTask;
    }
}