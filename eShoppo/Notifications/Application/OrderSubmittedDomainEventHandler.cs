using eShoppo.Orders.Domain;
using MediatR;

namespace eShoppo.Notifications.Application;

internal class OrderSubmittedDomainEventHandler : INotificationHandler<OrderSubmitted>
{
    private readonly ILogger<OrderSubmittedDomainEventHandler> _logger;

    public OrderSubmittedDomainEventHandler(ILogger<OrderSubmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderSubmitted notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Notification is sent for order {notification.SubmittedOrder}");
        return Task.CompletedTask;
    }
}