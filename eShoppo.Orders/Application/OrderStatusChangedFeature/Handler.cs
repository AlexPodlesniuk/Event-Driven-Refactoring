using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using eShoppo.Orders.Domain.OrderHistory;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Orders.Application.OrderStatusChangedFeature;

internal class Handler : IConsumer<OrderStatusChanged>
{
    private readonly Repository<OrderHistory> _repository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<OrderHistory> repository, ILogger<Handler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderStatusChanged> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Order history is created for order {context.Message}");
        
        var orderHistory = await _repository.GetById(message.OrderId) ?? new OrderHistory(message.OrderId);
        var newStatus = Enum.Parse<OrderStatus>(message.OrderStatus);
        orderHistory.Append(new HistoryItem(newStatus, message.SubmittedOn));
        
        await _repository.Save(orderHistory);
    }
}