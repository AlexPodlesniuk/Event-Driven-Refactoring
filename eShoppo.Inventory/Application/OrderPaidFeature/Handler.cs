using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderPaidFeature;

internal class Handler : IConsumer<OrderPaid>
{
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<OrderPaid> context)
    {
        var order = context.Message;
        var stockRequest = await _stockItemRequestRepository.GetById(order.OrderId) ?? throw new StockRequestIsNotFoundException(order.OrderId);
        
        stockRequest.ConfirmBooking();
        await _stockItemRequestRepository.Save(stockRequest);
        _logger.LogInformation($"Order {order.OrderNumber} paid, stock items will be decreased from inventory");
    }
}