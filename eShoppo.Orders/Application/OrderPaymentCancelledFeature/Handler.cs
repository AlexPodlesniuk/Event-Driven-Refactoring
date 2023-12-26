using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using eShoppo.Payments.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Orders.Application.OrderPaymentCancelledFeature;

internal class Handler : IConsumer<PaymentFailed>
{
    private readonly Repository<Order> _repository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<Order> repository, ILogger<Handler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentFailed> context)
    {
        var message = context.Message;
        var order = await _repository.GetById(message.OrderId) ?? throw new OrderNotFoundException(message.OrderId);
        _logger.LogInformation($"Order {order} payment expired, order will be marked as cancelled");

        order.Cancel();
        await _repository.Save(order);
    }
}