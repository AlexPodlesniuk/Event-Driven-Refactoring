using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using eShoppo.Payments.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderCancelledFeature;

public class Handler : IConsumer<OrderCancelled>
{
    private readonly IRequestClient<FindOrderRequest> _requestClient;
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;

    public Handler(IRequestClient<FindOrderRequest> requestClient, Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _requestClient = requestClient;
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = await _requestClient.GetResponse<FindOrderResponse>(new FindOrderRequest(context.Message.OrderId));

        await _stockItemRequestRepository.Remove(order.Message.OrderId);
        _logger.LogInformation($"Order {order.Message.OrderNumber} cancelled, stock item request will be released for order {order.Message}");
    }
}