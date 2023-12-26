using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderCancelledFeature;

internal class Handler : IConsumer<OrderCancelled>
{
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = context.Message;

        await _stockItemRequestRepository.Remove(order.OrderId);
        _logger.LogInformation($"Order {order.OrderNumber} cancelled, stock item request will be released");
    }
}