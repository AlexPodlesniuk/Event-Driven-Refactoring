using BuildingBlocks;
using eShoppo.Orders.Contracts;
using eShoppo.Orders.Domain.Order;
using eShoppo.Orders.Domain.OrderHistory;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Orders.Application.OrderCreatedFeature;

public class Handler : IConsumer<OrderCreated>
{
    private readonly ILogger<Handler> _logger;
    private readonly Repository<OrderHistory> _repository;

    public Handler(ILogger<Handler> logger, Repository<OrderHistory> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Order history is created for order {message}");
        
        var orderHistory = await _repository.GetById(message.Id) ?? new OrderHistory(message.Id);
        orderHistory.Append(new HistoryItem(OrderStatus.Created, message.CreatedOn));
        
        await _repository.Save(orderHistory);
    }
}