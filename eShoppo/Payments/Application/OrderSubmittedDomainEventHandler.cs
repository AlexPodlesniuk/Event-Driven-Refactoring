using eShoppo.Orders.Domain;
using MassTransit;

namespace eShoppo.Payments.Application;

public class OrderSubmittedDomainEventHandler : IConsumer<OrderSubmitted>
{
    private readonly ILogger<OrderSubmittedDomainEventHandler> _logger;

    public OrderSubmittedDomainEventHandler(ILogger<OrderSubmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        _logger.LogInformation($"Creating transaction for order {context.Message}");
        return Task.CompletedTask;
    }
}